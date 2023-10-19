using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class playerScript : MonoBehaviour
{
    // general statistics
    public float speed = 500;
    public float healthPoints = 5;
    private Vector2 movementVector;
    private int coins = 0;

    // dash statistics
    //public const float maxDashTime = 1.0f;
    //public float dashDistance = 10;
    //public float dashStoppingSpeed = 0.1f;
    //private float currentDashTime = maxDashTime;
    //private float dashSpeed = 6;

    // game entities
    public Rigidbody2D rb;
    public Camera cam;

    // Player input listeners setup
    PlayerInput input;

    private void Awake()
    {
        input = new PlayerInput();
    }

    private void OnEnable()
    {
        input.Enable();

        input.Gameplay.Movement.performed += OnMovement;
        input.Gameplay.Movement.canceled += OnMovement;
        //input.Gameplay.Dashing.performed += OnDashing;
    }

    private void OnDisable()
    {
        input.Disable();
    }

    // Player input handlers
    private void OnMovement(InputAction.CallbackContext context)
    {
        movementVector = context.ReadValue<Vector2>();
    }

    //private void OnDashing(InputAction.CallbackContext context)
    //{
    //    state = States.DASHING;
    //}

    // Update is called on its own timer
    void FixedUpdate()
    {
        rb.velocity = movementVector * speed * Time.fixedDeltaTime;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 7:     // enemy layer
                print("Enemy collision");
                healthPoints--;

                if (healthPoints == 0)
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
            case 10:     // coin layer
                print("Coin +1");
                coins++;
                break;
            default:
                break;
        }
    }
}