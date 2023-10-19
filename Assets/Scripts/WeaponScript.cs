using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponScript : MonoBehaviour
{
    public GameObject holder;
    public GameObject bulletPrefab;
    public BulletSpawner bulletSpawner;
    public float bulletSpeed;
    public Camera cam;
    public Rigidbody2D rb;

    private Vector2 mousePos;
    private Vector2 shootingVector;

    // Player input listeners
    PlayerInput input;

    private void Awake()
    {
        input = new PlayerInput();
    }

    private void OnEnable()
    {
        input.Enable();

        input.Gameplay.MousePos.performed += OnMousePos;

        input.Gameplay.Shooting.performed += OnMouseClick;
    }

    private void OnDisable()
    {
        input.Disable();
    }

    // Player input handlers
    private void OnMousePos(InputAction.CallbackContext context)
    {
        mousePos = cam.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }

    private void OnMouseClick(InputAction.CallbackContext context)
    {
        context.action.GetBindingForControl(context.control);
        Shoot();
    }

    // Logic calculations
    private void Update()
    {
        shootingVector = mousePos - rb.position;
    }

    void FixedUpdate()
    {
        // mates para rotar la pistola el angulo entre ella misma y el puntero del raton
        float angle = Mathf.Atan2(shootingVector.y, shootingVector.x) * Mathf.Rad2Deg;
        rb.MoveRotation(angle);
    }

    private void Shoot()
    {
        bulletSpawner.spawnBullet(bulletPrefab, shootingVector.normalized, bulletSpeed);
    }
}
