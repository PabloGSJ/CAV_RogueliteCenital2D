using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

/*
 * This class serves 2 purposes:
 *  - Save the context of the boss and pass it to the states
 *  - Update the boss context by performing the actions
 */
public class BossStateMachine : MonoBehaviour
{
    // CONTEXT:
    public Rigidbody2D rb;
    public BossDisplayManager ui;
    public CannonController cc;
    public HandsController hc;
    public PillarsController pc;
    private GameObject player;
    public BossRoom room;

    private const int PlayerLayer = 6;
    private const int PlayerBulletsLayer = 8;
    private const int BossLayer = 18;
    private const int BossInvulnerableLayer = 19;

    // State machine
    private BossBaseState _currentState;
    private BossStateFactory _states;
    public bool LoadArsenalStatic;

    // Health
    public int Health;
    public int MaxHealth;

    // Phase variables
    private bool _isSleeping;
    // cooldowns
    public float CannonCooldown;
    public float HandsCooldown;
    public float PillarsCooldown;



    // Getters setters
    public BossBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public GameObject Player { get { return player; } }
    public bool IsSleeping { get { return _isSleeping; } set { _isSleeping = value; } }



    // MONO BEHABIOUR FUNCTIONS:

    // Setup player systems
    private void Awake()            // called earlier thant Start()
    {
        // setup state
        _states = new BossStateFactory(this);
        _currentState = _states.Sleeping();
        _currentState.EnterState();
        IsSleeping = true;

        // setup health
        Health = MaxHealth;

        // setup player
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
            Debug.LogError("BOSS: player not found");

        // setup display manager
        ui.DisplayNewBossHealth(Health);
    }

    // update every game tick (very fast but irregular)
    private void Update()
    {
        _currentState.UpdateState();
    }

    // update for interactions involving physics engine
    void FixedUpdate()
    {
        
    }

    // Collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case PlayerLayer:
                break;
            case PlayerBulletsLayer:
                // TODO: play hit animation
                break;
            default:
                break;
        }
    }



    // AUXILIARY FUNCTION DECLARATIONS:

    public void TakeDamage(int damageTaken)
    {
        Health -= damageTaken;
        ui.DisplayNewBossHealth(Health);
        if (Health <= 0)
        {
            // die
            ui.DisplayNewBossHealth(0);
        }
    }

    public void SwitchBossToInvulnerableLayer(bool switchToInvulnerableLayer)
    {
        if (switchToInvulnerableLayer)
        {
            this.gameObject.layer = BossInvulnerableLayer;
        }
        else
        {
            this.gameObject.layer = BossLayer;
        }
    }
}
