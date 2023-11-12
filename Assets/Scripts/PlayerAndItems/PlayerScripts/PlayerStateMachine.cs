using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerStateMachine : MonoBehaviour
{
    public Rigidbody2D rb;
    public Camera cam;
    private PlayerInput _input;

    // Statistics variables
    public float Health = 5;
    private int _coins = 0;

    // Movement variables
    public float Speed = 10;
    private Vector2 _movementVector;
    private bool _isMoving = false;

    // Combat variables
    private GameObject _weapon = null;
    private GameObject _defaultWeapon = null;
    public const int WeaponsLayer = 10;
    public const int EnemyLayer = 11;

    // Collectibles variables
    public const int CoinLayer = 9;

    // Actions variables
    private bool _isInteracting;

    // States variables
    PlayerBaseState _currentState;
    PlayerStateFactory _states;

    // getters-setters
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public bool IsInteracting { get { return _isInteracting; } }
    public bool IsMoving { get { return _isMoving;  } }
    public Vector2 MovementVector { get { return _movementVector; } }

    // Called earlier than Start
    private void Awake()
    {
        // setup state
        _states = new PlayerStateFactory(this);
        _currentState = _states.Idle();
        _currentState.EnterState();

        // setup input system
        _input = new PlayerInput();
        _input.Gameplay.Movement.performed += OnMovement;
        _input.Gameplay.Movement.canceled += OnMovement;
        _input.Gameplay.Interact.performed += OnInteract;
        _input.Gameplay.Interact.canceled += OnInteract;
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

    // Performed when player moves
    private void OnMovement(InputAction.CallbackContext context)
    {
        _movementVector = context.ReadValue<Vector2>();
        _isMoving = context.performed;
    }

    // Performed when player interacts with something
    private void OnInteract(InputAction.CallbackContext context)
    {
        _isInteracting = context.ReadValueAsButton();
    }

    /*    */
    private void Update()
    {
        _currentState.UpdateState();
    }

    void FixedUpdate()
    {
        rb.velocity = _movementVector * Speed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case EnemyLayer:
                print("Enemy collision");
                Health--;

                if (Health == 0)
                {
                    // die
                    Destroy(gameObject);
                }

                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case CoinLayer:     // coin layer
                print("Coin +1");
                _coins++;
                break;
            case WeaponsLayer:    // weapons layer
                // player can press E to pickup
                _isInteracting = false;
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
                if (_isInteracting)
                {
                    if (_weapon != null)
                    {
                        // drop the old weapon
                        StartCoroutine(_weapon.GetComponent<WeaponScript>().drop());
                    }

                    // pick up the new weapon
                    collision.gameObject.GetComponent<WeaponScript>().pickUp(gameObject);
                    _weapon = collision.gameObject;
                }
                break;

            default:
                break;
        }
    }
}
