using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerScript : MonoBehaviour
{
    // General
    public Rigidbody2D rb;
    public Camera cam;
    PlayerInput _input;

    // Stats
    public float Speed = 10;
    public float Health = 5;
    private Vector2 _movementVector;
    private int _coins = 0;

    // weapon
    private GameObject _weapon = null;

    // Logic
    private bool _pressedE;


    // Manage player inputs
    private void Awake()
    {
        _input = new PlayerInput();
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        _movementVector = context.ReadValue<Vector2>();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        _pressedE = true;
    }

    // Transform
    

    
}