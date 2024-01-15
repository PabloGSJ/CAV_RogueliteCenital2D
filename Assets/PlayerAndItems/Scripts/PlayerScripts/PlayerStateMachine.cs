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
public class PlayerStateMachine : MonoBehaviour
{
    // CONTEXT:

    public Rigidbody2D rb;
    public Camera cam;
    private PlayerInput _input;
    private Vector2 _mousePos;
    private DisplayManager ui;

    public const int CoinsLayer = 9;
    public const int WeaponsLayer = 10;
    public const int EnemiesLayer = 11;
    public const int EnemyBulletsLayer = 12;
    public const int GroundBulletsLayer = 13;
    public const int HeartsLayer = 14;
    public const int GMLayer = 15;

    // Statistics variables
    public const int MaxHealth = 5;
    public int Health = 3;
    private int _coins = 0;

    // Movement variables
    public float Speed = 500;
    private Vector2 _movementVector;
    private bool _dashing = false;
    public float DashSpeed = 750;
    public float DashCooldown;
    private float _dashCooldownCounter;
    public float DashDuration;

    // Combat variables
    public BaseWeapon Weapon = null;
    public GameObject DefaultWeapon = null;
    private int _numBullets = 99;
    private float _dmgMod = 0;

    // Actions variables
    private bool _interacted;

    // States variables
    PlayerBaseState _currentState;
    PlayerStateFactory _states;

    // getters-setters
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public bool IsInteracting { get { return _interacted; } }
    public bool IsDashing { get { return _dashing; } }
    public Vector2 MovementVector { get { return _movementVector; } }
    public Vector2 MousePos { get { return _mousePos; } }
    public float DmgMod { set { _dmgMod = value; } }


    // INPUT HANDLERS:

    // INPUT: Performed when player moves
    private void OnMovement(InputAction.CallbackContext context)
    {
        _movementVector = context.ReadValue<Vector2>();
    }

    // INPUT: Performed when player interacts with something
    private void OnInteract(InputAction.CallbackContext context)
    {
        _interacted = context.ReadValueAsButton();
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        context.action.GetBindingForControl(context.control);
        if (context.ReadValueAsButton() && Weapon != null)
        {
            Debug.Log("Shoot!");
            Weapon.Shoot(_dmgMod);
        }
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        if (_dashCooldownCounter <= 0)
        {
            _dashing = true;
        }
    }


    // MONO BEHABIOUR FUNCTIONS:

    // Setup player systems
    private void Awake()            // called earlier thant Start()
    {
        // setup state
        _states = new PlayerStateFactory(this);
        _currentState = _states.Running();
        _currentState.EnterState();

        // setup logic manager
        ui = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<DisplayManager>();
        ui.DisplayNewHealth(Health);
        ui.DisplayNewPNBullets(_numBullets);
        ui.EnableWeaponNBullets(false);
        ui.DisplayNewPCoins(_coins);

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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    // Triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case CoinsLayer:
                PickupCoin();
                break;
            case WeaponsLayer:
                _interacted = false;    // reset actions and listen
                break;
            case GroundBulletsLayer:
                PickupGroundBullet();
                break;
            case HeartsLayer:
                PickupHeart();
                break;
            case GMLayer:
                Debug.Log("Picked up gm");
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

            default:
                break;
        }
    }


    // AUXILIARY FUNCTION DECLARATIONS:

    /*
     * Borrows one bullet from player inventory
     */

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

    private void PickupCoin()
    {
        _coins++;
        ui.DisplayNewPCoins(_coins);
    }

    private void PickupGroundBullet()
    {
        _numBullets++;
        ui.DisplayNewPNBullets(_numBullets);
    }

    private void PickupHeart()
    {
        if (Health < MaxHealth)
        {
            // the heart gets consumed anyway
            Health++;
            ui.DisplayNewHealth(Health);
        }
    }

    private void PickupWeapon(BaseWeapon weapon)
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
}
