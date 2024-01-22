using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

/*
 * This class serves 2 purposes:
 *  - Save the context of the player and pass it to the states
 *  - Update the player context by performing the actions
 */
public class BossStateMachine : MonoBehaviour
{
    // CONTEXT:


    // Health
    public int Health;
    public int MaxHealth;

    // State machine
    private BossBaseState _currentState;
    private BossStateFactory _states;



    // Phase variables
    private float _phase1Counter;
    private float _phase2Counter;
    private float _phase3Counter;

    // Getters setters
    public float Phase1Counter { get { return _phase1Counter; } set { _phase1Counter = value; } }
    public float Phase2Counter { get { return _phase2Counter; } set { _phase2Counter = value; } }
    public float Phase3Counter { get { return _phase3Counter; } set { _phase3Counter = value; } }
    


    // MONO BEHABIOUR FUNCTIONS:

    // Setup player systems
    private void Awake()            // called earlier thant Start()
    {
        // setup state
        _states = new BossStateFactory(this);
        _currentState = _states.Phase1();
        _currentState.EnterState();

        // setup logic manager
        ui = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<DisplayManager>();
        ui.DisplayNewHealth(Health);
        ui.DisplayNewPNBullets(_numBullets);
        ui.EnableWeaponNBullets(false);
        ui.DisplayNewPCoins(_coins);
        ui.DisplayNewMaxHealth(MaxHealth);

        // setup input system
        _input = new PlayerInput();
        _input.Gameplay.Movement.performed += OnMovement;
        _input.Gameplay.Movement.canceled += OnMovement;
        _input.Gameplay.Interact.performed += OnInteract;
        _input.Gameplay.Interact.canceled += OnInteract;
        _input.Gameplay.Shooting.performed += OnShoot;
        _input.Gameplay.Shooting.canceled += OnShoot;
        _input.Gameplay.Dash.performed += OnDash;
        _input.Gameplay.Dash.canceled += OnDash;

        // setup default weapon
        GameObject go = Instantiate(DefaultWeapon,
                                    new Vector3(transform.position.x,
                                                transform.position.y,
                                                transform.position.z),
                                    Quaternion.identity,
                                    this.transform) as GameObject;
        PickupWeapon(go.GetComponent<BaseWeapon>());

        // setup dash
        _dashCooldownCounter = DashCooldown;
    }

    // setup input system
    private void OnEnable()
    {
        _input.Enable();
    }
    private void OnDisable()
    {
        _input.Disable();
    }

    // update every game tick (very fast but irregular)
    private void Update()
    {
        // INPUT: read mouse position on screen
        _mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        // reduce dash cooldown
        if (!_dashing && _dashCooldownCounter > 0)
        {
            _dashCooldownCounter -= Time.deltaTime;
            ui.EnableDashCooldown(true);
            ui.DisplayNewDashCooldown(_dashCooldownCounter);
        }
        else
        {
            ui.EnableDashCooldown(false);
        }
    }

    // update for interactions involving physics engine
    void FixedUpdate()
    {
        _currentState.UpdateState();
    }

    // Collisions
    private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<BolaDeFuegoScript>())
            {
                BolaDeFuegoScript playerHealth = other.gameObject.GetComponent<BolaDeFuegoScript>();
                playerHealth.TakeDamage(damage);
            }
        }

    // Triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case ShopItemsLayer:
            case WeaponsLayer:
                _interacted = false;    // reset actions and listen
                break;

            case GMLayer:
                Debug.Log("Picked up gm");
                break;

            case Consumables:
                // update all consumable related statistics just in case
                UpdateConsumables();
                break;

            default:
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case WeaponsLayer:    // weapons layer
                if (_interacted)
                {
                    PickupWeapon(collision.gameObject.GetComponent<BaseWeapon>());
                }
                break;

            case ShopItemsLayer:
                if (_interacted)
                {
                    BaseShopItem shopItem = collision.gameObject.GetComponent<BaseShopItem>();
                    if (shopItem.TryBuy(_coins))
                    {
                        // Player has enough coins to buy 
                        //shopItem.BuyItem(this);
                        //shopItem.BuyItem(this);
                    }
                }
                break;

            default:
                break;
        }
    }


    // AUXILIARY FUNCTION DECLARATIONS:

    public void MovePlayer()
    {
        rb.velocity = _movementVector * Speed * Time.fixedDeltaTime;
    }

    public void TakeDamage(int damageTaken)
    {
        Debug.Log("Recieving Damage");

        Health -= damageTaken;
        if (Health <= 0)
        {
            // die
            ui.DisplayNewHealth(0);
            Destroy(gameObject);
        }
        ui.DisplayNewHealth(Health);
    }

    public void PickupWeapon(BaseWeapon weapon)
    {
        if (Weapon != null)
        {
            // drop the old weapon
            StartCoroutine(Weapon.Dropped());
        }

        // pick up the new weapon
        weapon.Pickedup(gameObject.GetComponent<PlayerStateMachine>());
        Weapon = weapon;
    }

    public bool BorrowBullet()
    {
        bool borrow = false;
        if (_numBullets > 0)
        {
            borrow = true;
            _numBullets--;
            ui.DisplayNewPNBullets(_numBullets);
        }
        return borrow;
    }

    public void ResetDash()
    {
        _dashing = false;
        _dashCooldownCounter = DashCooldown;
    }

    public void UpdateConsumables()
    {
        ui.DisplayNewHealth(Health);
        ui.DisplayNewPCoins(_coins);
        ui.DisplayNewPNBullets(_numBullets);
    }

    public void SwitchPlayerToInvulnerableLayer(bool switchToInvulnerableLayer)
    {
        if (switchToInvulnerableLayer)
        {
            this.gameObject.layer = PlayerInvulnerableLayer;
        }
        else
        {
            this.gameObject.layer = PlayerLayer;
        }
    }
}
