using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    public GameObject player;
    
    void Start()
    {
        // Obtener el componente Rigidbody2D del jugador
        rb = GetComponent<Rigidbody2D>();

        // Desactivar la gravedad del Rigidbody2D
        rb.gravityScale = 0;

        if (player == null)
        {
            Debug.LogError("Player reference not set on " + gameObject.name);
        }
    }

    void Update()
    {
        // Obtener entrada del jugador
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize(); // Normaliza el vector para movimiento diagonal consistente
    }

    void FixedUpdate()
    {
        // Mover el jugador en FixedUpdate
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
    }
}