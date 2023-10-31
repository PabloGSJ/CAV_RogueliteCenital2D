using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class playerScript : MonoBehaviour
{
    // General
    public Rigidbody2D rb;
    public Camera cam;
    PlayerInput input;

    // Stats
    public float speed = 10;
    public float healthPoints = 5;
    private Vector2 movementVector;
    private int coins = 0;

    // weapon
    private GameObject weapon = null;

    // Logic
    private bool pressedE;


    // Manage player inputs
    private void Awake()
    {
        input = new PlayerInput();
    }

    private void OnEnable()
    {
        input.Enable();

        input.Gameplay.Movement.performed += OnMovement;
        input.Gameplay.Movement.canceled += OnMovement;
        input.Gameplay.Interact.performed += OnInteract;
        input.Gameplay.Interact.canceled += OnInteract;
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        movementVector = context.ReadValue<Vector2>();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        pressedE = true;
    }

    // Transform
    void FixedUpdate()
    {
        rb.velocity = movementVector * speed * Time.fixedDeltaTime;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 11:     // enemy layer
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
            case 9:     // coin layer
                print("Coin +1");
                coins++;
                break;
            case 10:    // weapons layer
                // player can press E to pickup
                pressedE = false;
                break;

            default:
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        { 
            case 10:    // weapons layer
                if (pressedE)
                {
                    if (weapon != null)
                    {
                        // drop the old weapon
                        StartCoroutine(weapon.GetComponent<WeaponScript>().drop());
                    }
                    
                    // pick up the new weapon
                    collision.gameObject.GetComponent<WeaponScript>().pickUp(gameObject);
                    weapon = collision.gameObject;
                }
                break;

            default:
                break;
        }
    }
}