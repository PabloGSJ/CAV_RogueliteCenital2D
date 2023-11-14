using System.Collections;
using System.Linq;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float moveDuration = 1.0f;
    public float shootDuration = 3.0f;
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    public float margin = 1f;
    public Color firingColor = Color.yellow;
    private Vector3[] corners;
    private SpriteRenderer spriteRenderer;
    private Transform player;
    private System.Random random = new System.Random();
    



    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").transform;
        if (spriteRenderer == null || player == null)
        {
            Debug.LogError("BossMovement: No se encontró un SpriteRenderer o el prefab Player en el objeto.");
            enabled = false;
        }

        CalculateCornersWithMargin();
        StartCoroutine(RandomMovement());
    }

    void CalculateCornersWithMargin()
    {
        Camera cam = Camera.main;
                corners = new Vector3[]
                {
                    cam.ViewportToWorldPoint(new Vector3(margin / cam.pixelWidth, margin / cam.pixelHeight, cam.nearClipPlane)),
                    cam.ViewportToWorldPoint(new Vector3(margin / cam.pixelWidth, 1 - (margin / cam.pixelHeight), cam.nearClipPlane)),
                    cam.ViewportToWorldPoint(new Vector3(1 - (margin / cam.pixelWidth), 1 - (margin / cam.pixelHeight), cam.nearClipPlane)),
                    cam.ViewportToWorldPoint(new Vector3(1 - (margin / cam.pixelWidth), margin / cam.pixelHeight, cam.nearClipPlane))
                };
    }

    IEnumerator RandomMovement()
        {
            while (true)
            {
                Vector3 targetPosition = corners[random.Next(corners.Length)];
                yield return StartCoroutine(MoveToPosition(targetPosition));
                spriteRenderer.flipX = (targetPosition.x < transform.position.x);
                StartCoroutine(ChangeColorTemporary(firingColor, 0.5f));
                yield return StartCoroutine(ShootRandomly());
            }
        }

    IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        float timeElapsed = 0;
        Vector3 startPosition = transform.position;
        
        while (timeElapsed < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed / moveDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition; // Asegurarse de que el boss llegue a la posición.
    }

    

    IEnumerator ChangeColorTemporary(Color color, float duration)
    {
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = color;
        yield return new WaitForSeconds(duration);
        spriteRenderer.color = originalColor;
    }


     IEnumerator ShootRandomly()
    {
        float endTime = Time.time + shootDuration;
        int shots = RandomWeightedNumber();
        for (int i = 0; i < shots; i++)
        {
            if (Time.time >= endTime)
                break;
            
            // Select a random shooting pattern
            int pattern = random.Next(3);
            switch (pattern)
            {
                case 0:
                    FireInAllDirections();
                    break;
                case 1:
                    FireConeTowardsPlayer();
                    break;
                case 2:
                    FireStraightAtPlayer();
                    break;
            }

            yield return new WaitForSeconds(shootDuration / shots);
        }
    }

    void FireInAllDirections()
    {
        int bulletAmount = 30;
        float angleStep = 360f / bulletAmount;
        float angle = 0f;

        for (int i = 0; i < bulletAmount; i++)
        {
            // Calcula la dirección de este disparo basándose en el ángulo
            Quaternion bulletRotation = Quaternion.Euler(0f, 0f, angle);
            FireBulletInDirection(bulletRotation);

            // Incrementa el ángulo para el próximo disparo
            angle += angleStep;
        }
    }

    void FireConeTowardsPlayer()
    {
    int bulletAmount = 10;
        Vector2 directionToPlayer = GetDirectionToPlayer();
        
        float startAngle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        for (int i = 0; i < bulletAmount; i++)
        {
            float angle = startAngle + (i - bulletAmount / 2) * 10f;
            Quaternion bulletRotation = Quaternion.Euler(0f, 0f, angle);
            FireBulletInDirection(bulletRotation);
        }
    }

    void FireStraightAtPlayer()
    {
        Quaternion bulletRotation = Quaternion.LookRotation(Vector3.forward, GetDirectionToPlayer());
        FireBulletInDirection(bulletRotation);
    }

    Vector2 GetDirectionToPlayer()
    {
        // Asumimos que 'player' es una referencia válida al GameObject del jugador.
        return (player.transform.position - transform.position).normalized;
    }


    void FireBulletInDirection(Quaternion rotation)
    {
        // Crea un nuevo proyectil en la posición del jefe con la rotación dada
        GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Calcula la dirección correcta
        Vector2 direction = rotation * Vector2.right;

        // Asegúrate de que 'bulletPrefab' tenga un componente Rigidbody2D
        if (rb != null)
        {
            // Dispara el proyectil en la dirección calculada
            rb.velocity = direction * bulletSpeed;
        }
        else
        {
        Debug.LogError("El prefab de la bala no tiene un componente Rigidbody2D.");
        }
    }

    int RandomWeightedNumber()
    {
        // Números del 1 al 3, con una mayor probabilidad para 1, seguido de 2, y menos para 3.
        int[] numbers = { 1, 1, 1, 1, 2, 2, 3 }; // 1 es cuatro veces más probable que 3.
        int index = random.Next(numbers.Length);
        return numbers[index];
    }

        private void ReflectSpriteBasedOnPosition()
    {
        // Esto asume que tu mapa está centrado en el origen (0,0)
        // y que el lado izquierdo del mapa tiene valores de X negativos.
        if (transform.position.x < 0)
        {
            // Estamos en el lado izquierdo del mapa, refleja el sprite en el eje X
            spriteRenderer.flipX = true;
        }
        else
        {
            // Estamos en el lado derecho del mapa, asegúrate de que el sprite no esté reflejado
            spriteRenderer.flipX = false;
        }
    }

    void Update()
    {
        //GetDirectionToPlayer();
        ReflectSpriteBasedOnPosition();
    }
}
