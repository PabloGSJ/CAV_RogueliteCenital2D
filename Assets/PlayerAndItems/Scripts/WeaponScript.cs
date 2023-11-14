using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponScript : MonoBehaviour
{
    // General
    public Camera cam;
    public Rigidbody2D rb;
    PlayerInput input;
    public CircleCollider2D trig;

    // weapon usage
    private GameObject holder;
    public float holdOffset;
    private Vector2 mousePos;
    private Vector2 shootingVector;

    // shooting
    public GameObject bulletPrefab;
    public BulletSpawnerScript bulletSpawner;
    public float bulletSpeed;

    private void Awake()
    {
        input = new PlayerInput();
        holder = null;
    }

    private void OnEnable()
    {
        input.Enable();

        //input.Gameplay.MousePos.performed += OnMousePos;

        //input.Gameplay.Shooting.performed += OnMouseClick;
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

    // Logic 
    private void Update()
    {
        shootingVector = mousePos - rb.position;
    }

    // Transform 
    void FixedUpdate()
    {
            // mates para rotar la pistola el angulo entre ella misma y el puntero del raton
            float angle = Mathf.Atan2(shootingVector.y, shootingVector.x) * Mathf.Rad2Deg;
        if (holder != null)
        {
            rb.MoveRotation(angle);
        }
    }

    private void Shoot()
    {
        bulletSpawner.spawnBullet(bulletPrefab, shootingVector.normalized, bulletSpeed);
    }

    // sets a new holder for the weapon
    public void pickUp(GameObject newHolder)
    {
        trig.enabled = false;
        this.holder = newHolder;
        this.transform.parent = newHolder.transform;
        this.transform.position = new Vector3(newHolder.transform.position.x + holdOffset, newHolder.transform.position.y, 0);

        input.Gameplay.MousePos.performed += OnMousePos;
        input.Gameplay.Shooting.performed += OnMouseClick;
    }

    public IEnumerator drop()
    {
        this.holder = null;
        this.transform.parent = null;

        input.Gameplay.MousePos.performed -= OnMousePos;
        input.Gameplay.Shooting.performed -= OnMouseClick;
        yield return new WaitForSeconds(1f);
        trig.enabled = true;
    }
}
