using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.SceneManagement;

/*
 * This class serves 2 purposes:
 *  - Save the context of the player and pass it to the states
 *  - Update the player context by performing the actions
 */
public class PlayerStateMachine : MonoBehaviour
{
    // CONTEXT:

    public Rigidbody2D rb;
    private Camera cam;
    private PlayerInput _input;
    private Vector2 _mousePos;
    private DisplayManager ui;
    public SpriteRenderer sr;
    public SpriteRenderer e;
    public Animator a;
    public GameObject pr;
    private SoundControllerScript sc;
    public GameObject gameOverCanvas;
    public Animator gameOverAnim;
    public GameObject pauseMenuCanvas;
    private bool _isPaused;
    public BaseGM[] gms;
    public Sprite[] DeathSpriteSequence;
    public Collider2D mycoll;

    private const int PlayerLayer           = 6;
    private const int WallsLayer            = 7;
    private const int Consumables           = 9;
    private const int WeaponsLayer          = 10;
    private const int EnemiesLayer          = 11;
    private const int EnemyBulletsLayer     = 12;
    private const int ShopItemsLayer        = 13;
    private const int PlayerDashingLayer    = 14;
    private const int GMLayer               = 15;
    private const int ClassSelectorLayer    = 16;
    private const int ChestLayer            = 17;
    private const int SceneChangeLayer      = 21;

    private const string AIsMoving    = "IsMoving";
    private const string AIsDashing   = "IsDashing";
    private const string AIsDead      = "IsDead";

    // Statistics variables
    public int MaxHealth = 10;  // constant
    public int Health = 3;
    public int MaxCoins = 99;
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
    public int MaxPBullets = 99;
    private int _numBullets = 0;
    private float _dmgMod = 0;
    private bool _isDamaged = false;
    private float _isDamagedCounter;
    public float InvulnerableTime;
    private bool _isDead;

    // Actions variables
    private bool _interacted;

    // States variables
    PlayerBaseState _currentState;
    PlayerStateFactory _states;
    private bool _isGod;

    // GM variables
    private int _damageModifier = 1;
    private bool[] _activeGM = { false, false, false, false, false };
    private enum GMnames { EnemyDamageUp, MaxHealthHalved, ShorterDash, SpeedDown, Take10Bullets };

    // getters-setters
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public bool IsInteracting { get { return _interacted; } }
    public bool IsDashing { get { return _dashing; } }
    public Vector2 MovementVector { get { return _movementVector; } }
    public Vector2 MousePos { get { return _mousePos; } }
    public float DmgMod { set { _dmgMod = value; } }
    public int Coins { get { return _coins; } set { _coins = value; } }
    public int NumBullets { get { return _numBullets; } set { _numBullets = value; } }
    public bool IsDamaged { get { return _isDamaged; } }
    public string AnimIsMoving { get { return AIsMoving; } }
    public string AnimIsDashing { get { return AIsDashing; } }
    public string AnimIsDead { get { return AIsDead; } }
    public SoundControllerScript SoundController { get { return sc; } }
    public bool IsDead { get { return _isDead; } }
    public int DamageModifier { set { _damageModifier = value; } }
    public bool IsPaused { set { _isPaused = value; } }


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
        if (context.ReadValueAsButton() && Weapon != null && !_isPaused)
        {
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

    private void OnPause(InputAction.CallbackContext context)
    {
        if (_isPaused)
        {
            // unpause
            pauseMenuCanvas.SetActive(false);
            Time.timeScale = 1;
            _isPaused = false;
        }
        else
        {
            // pause
            Time.timeScale = 0;
            pauseMenuCanvas.SetActive(true);
            _isPaused = true;
        }
    }


    // MONO BEHABIOUR FUNCTIONS:

    // Setup player systems
    private void Start()            // called last during initialization
    {
        // get the camera
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        if (cam == null)
        {
            Debug.LogError("PLAYER: Camera not found");
        }

        // check that there is a proper EMPTY game object for the bullets
        GameObject empty = GameObject.FindGameObjectWithTag("Empty");
        if (empty == null)
        {
            Debug.LogError("PLAYER: \"Empty\" game object not found");
        }

        // get the sound controller
        sc = GameObject.FindGameObjectWithTag("SoundControl").GetComponent<SoundControllerScript>();
        if (sc == null)
        {
            Debug.LogError("PLAYER: Sound controller not found");
        }

        // setup state
        _isDead = false;
        _isPaused = false;
        _states = new PlayerStateFactory(this);
        _currentState = _states.Running();
        _currentState.EnterState();
        a.SetBool(AIsMoving, false);
        a.SetBool(AIsDashing, false);
        a.SetBool(AIsDead, false);
        _isGod = false;
        _isDamagedCounter = InvulnerableTime;

        // Setup canvases
        gameOverCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(false);

        // setup logic manager
        ui = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<DisplayManager>();
        ui.DisplayNewPNBullets(_numBullets);
        ui.EnableWeaponNBullets(false);
        ui.DisplayNewPCoins(_coins);
        ui.MaxHealth = this.MaxHealth;
        this.Health = this.MaxHealth;
        ui.ActiveHearts = 5;
        ui.DisplayNewHealth(Health);
        ui.EnableIsGodDisplay(_isGod);

        // setup dash
        _dashCooldownCounter = 0;

        // read state from PlayerData
        LoadState();

        if (Weapon == null)
        {
            // setup default weapon
            GameObject go = Instantiate(DefaultWeapon,
                                        new Vector3(transform.position.x,
                                                    transform.position.y,
                                                    transform.position.z),
                                        Quaternion.identity,
                                        this.transform) as GameObject;
            PickupWeapon(go.GetComponent<BaseWeapon>());
        }
    }

    private void Awake()
    {
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
        _input.Gameplay.Pause.performed += OnPause;
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
        }

        if (_isDamaged)
        {
            // player damaged
            _isDamagedCounter -= Time.deltaTime;

            SwitchPlayerToDashLayer(true);
            sr.color = Color.gray;

            if (_isDamagedCounter <= 0)
            {
                // player no longer damaged
                _isDamagedCounter = InvulnerableTime;
                sr.color = Color.white;

                if (!_dashing)
                {
                    _isDamaged = false;
                    SwitchPlayerToDashLayer(false);
                }
            }
        }

        // if godmode is active, keep switching the player to the invulnerable layer
        if (_isGod)
        {
            SwitchPlayerToDashLayer(true);
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
        switch (collision.gameObject.layer)
        {
            case EnemiesLayer:
            case EnemyBulletsLayer:
                break;

            case WallsLayer:
                sc.playHitAWallSoundEffect();
                break;

            default:
                break;
        }
    }

    // Triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case ShopItemsLayer:
            case ClassSelectorLayer:
            case ChestLayer:
            case WeaponsLayer:
            case SceneChangeLayer:
                _interacted = false;    // reset actions and listen
                e.enabled = true;
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
        if (_interacted)
        {
            switch (collision.gameObject.layer)
            {
                case WeaponsLayer:    // weapons layer
                    PickupWeapon(collision.gameObject.GetComponent<BaseWeapon>());
                    break;

                case ShopItemsLayer:
                    BaseShopItem shopItem = collision.gameObject.GetComponent<BaseShopItem>();
                    if (shopItem.TryBuy(_coins))
                    {
                        // Player has enough coins to buy 
                        shopItem.BuyItem(this);
                    }
                    break;

                case ClassSelectorLayer:
                    Class c = collision.gameObject.GetComponent<Class>();
                    c.SelectClass(this);
                    break;

                case ChestLayer:
                    Chest chest = collision.gameObject.GetComponent<Chest>();
                    int gmid = chest.OpenChest(this);
                    _coins += 10;
                    if (gmid != -1 && !_activeGM[gmid])
                    {
                        gms[gmid].UseGM(this);
                        _activeGM[gmid] = true;
                        UpdateConsumables();
                    }
                    break;
                case SceneChangeLayer:
                    collision.gameObject.GetComponent<SceneChanger>().StartSceneChange();
                    break;

                default:
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case ShopItemsLayer:
            case ClassSelectorLayer:
            case ChestLayer:
            case WeaponsLayer:
            case SceneChangeLayer:
                _interacted = false;    // reset actions and listen
                e.enabled = false;
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
        Health -= (damageTaken * _damageModifier);
        if (Health <= 0)
        {
            // die
            ui.DisplayNewHealth(0);
            _isDead = true;
        }
        _isDamaged = true;
        ui.DisplayNewHealth(Health);
        sc.playPlayerDamagedSoundEffect();
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

    public bool TryBorrowBullet()
    {
        if (_numBullets <= 0)
        {
            // Drop the weapon and start punching
            GameObject go = Instantiate(DefaultWeapon,
                                        new Vector3(transform.position.x,
                                                    transform.position.y,
                                                    transform.position.z),
                                        Quaternion.identity,
                                        this.transform) as GameObject;
            PickupWeapon(go.GetComponent<BaseWeapon>());
        }
        return _numBullets > 0;
    }

    public bool BorrowBullet()
    {
        bool borrow = _numBullets > 0;
        if (borrow)
        { 
            _numBullets--;
            ui.DisplayNewPNBullets(_numBullets);
        }
        return borrow;
    }

    public void UpdateConsumables()
    {
        ui.DisplayNewHealth(Health);
        ui.DisplayNewPCoins(_coins);
        ui.DisplayNewPNBullets(_numBullets);
    }

    public void UpdateMaxHealth()
    {
        ui.ActiveHearts = MaxHealth / ui.HealthPerHeart;

        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        ui.DisplayNewHealth(Health);
    }

    public void ResetDash()
    {
        _dashing = false;
        _dashCooldownCounter = DashCooldown;
    }

    public void SwitchPlayerToDashLayer(bool switchToDashLayer)
    {
        if (switchToDashLayer)
        {
            
            this.gameObject.layer = PlayerDashingLayer;
        }
        else
        {
            this.gameObject.layer = PlayerLayer;
        }
    }

    public void Wait(float seconds)
    {
        StartCoroutine(wait(seconds));
    }

    private IEnumerator wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    public void SaveState()
    {
        PlayerData pd = SceneManager.GetSceneByName("MainScene").GetRootGameObjects()[0].GetComponent<PlayerData>();

        pd.SetWeapon(this.Weapon);
        pd.Health = this.Health;
        pd.Coins = this._coins;
        pd.Bullets = this._numBullets;
        pd.ActiveGM = this._activeGM;
    }

    public void LoadState()
    {
        PlayerData pd = SceneManager.GetSceneByName("MainScene").GetRootGameObjects()[0].GetComponent<PlayerData>();

        GameObject loadedWeapon = pd.GetWeapon();

        if (loadedWeapon != null)
        {
            GameObject go = Instantiate(loadedWeapon,
                                    new Vector3(transform.position.x,
                                                transform.position.y,
                                                transform.position.z),
                                    Quaternion.identity,
                                    this.transform) as GameObject;
            PickupWeapon(go.GetComponent<BaseWeapon>());
        }
        this.Health = pd.Health;
        this._coins = pd.Coins;
        this._numBullets = pd.Bullets;
        UpdateConsumables();
        this._activeGM = pd.ActiveGM;

        for (int i = 0; i < gms.Length; i++)
        {
            if (_activeGM[i])
            {
                gms[i].UseGM(this);
            }
        }
    }

    public void ToggleGodMode()
    {
        if (_isGod)
        {
            // Ungod the player
            _isGod = false;
            SwitchPlayerToDashLayer(false);
        }
        else
        {
            // God the player
            _isGod = true;
        }

        ui.EnableIsGodDisplay(_isGod);
    }
}
