using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    // CONTEXTO:

    private SpriteRenderer spriteRenderer;
    private Transform player;
    private System.Random random = new System.Random();

    // Stats variables
    public float health = 1000;  // Agrega un valor de vida

    // Movement variables
    public float moveDuration = 1.0f;
    private bool isMoving;
    private Vector3[] edgeMidpoints;
    private Vector3[] corners;
    public float margin = 1f;

    // Combat variables
    public float shootDuration = 3.0f;
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    public Color firingColor = Color.yellow;
    private Coroutine trembleCoroutine;

    // State machine variables
    private enum BossState { RandomMovement, CutMovement, PunchMovement } // Agrega estados de comportamiento
    private BossState currentState;
    private Coroutine currentBehaviorCoroutine;

    // MONO BEHAVIOUR FUNCTIONS:

    // Executed at the declaration of the Game Object
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
        CalculateEdgeMidpoints();
        SetInitialBossState(); // Establece e inicia el estado inicial basado en la salud
    }

    // Executed once every frame
    void Update()
    {
        // Actualizaciones regulares, como ReflectSpriteBasedOnPosition
        UpdateBossState();
        ReflectSpriteBasedOnPosition();
    }


    // AUXILIARY FUNCTIONS:

    void CalculateEdgeMidpoints()
    {
        Camera cam = Camera.main;
        edgeMidpoints = new Vector3[]
        {
            cam.ViewportToWorldPoint(new Vector3(0.5f, margin / cam.pixelHeight, cam.nearClipPlane)), // Arriba
            cam.ViewportToWorldPoint(new Vector3(0.5f, 1 - (margin / cam.pixelHeight), cam.nearClipPlane)), // Abajo
            cam.ViewportToWorldPoint(new Vector3(margin / cam.pixelWidth, 0.5f, cam.nearClipPlane)), // Izquierda
            cam.ViewportToWorldPoint(new Vector3(1 - (margin / cam.pixelWidth), 0.5f, cam.nearClipPlane)) // Derecha
        };
    }

    IEnumerator CutMovement()
    {
        while (currentState == BossState.CutMovement)
        {
            Vector3 startPoint = ChooseRandomPoint();
            Vector3 endPoint = ChooseOppositePoint(startPoint);

            yield return StartCoroutine(MoveToPosition(startPoint));
            yield return StartCoroutine(MoveToPosition(endPoint));
            
            yield return new WaitForSeconds(1f);
        }
    }

    Vector3 ChooseRandomPoint()
    {
        List<Vector3> allPoints = new List<Vector3>(corners);
        allPoints.AddRange(edgeMidpoints);
        int randomIndex = random.Next(allPoints.Count);
        return allPoints[randomIndex];
    }

    private Vector3 ChooseOppositePoint(Vector3 startPoint)
    {
        // Determinar el punto opuesto en la pantalla
        Vector3 oppositePoint;
        if (corners.Contains(startPoint))
        {
            int startIndex = Array.IndexOf(corners, startPoint);
            oppositePoint = corners[(startIndex + 2) % 4]; // Elige la esquina opuesta
        }
        else
        {
            int startIndex = Array.IndexOf(edgeMidpoints, startPoint);
            oppositePoint = edgeMidpoints[(startIndex + 2) % 4]; // Elige el punto medio opuesto
        }

        return oppositePoint;
    }
    IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        // Cambiar el color a amarillo antes de comenzar a moverse
        StartCoroutine(ChangeColorTemporary(Color.yellow, 0.5f));
        yield return new WaitForSeconds(0.5f);

        float timeElapsed = 0;
        Vector3 startPosition = transform.position;

        // Cambiar el color a rojo mientras se mueve
        spriteRenderer.color = Color.red;

        while (timeElapsed < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed / moveDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;

        // Restablecer el color original después del movimiento
        spriteRenderer.color = Color.white;
    }

    void SetInitialBossState()
    {
        if (health > 660)
        {
            ChangeState(BossState.PunchMovement);
        }
        else if (health >= 330 && health < 660)
        {
            ChangeState(BossState.CutMovement);
        }
        else
        {
            ChangeState(BossState.RandomMovement);
        }
    }

    void UpdateBossState(bool forceUpdate = false)
    {
        BossState newState;
        if (health > 660)
        {
            newState = BossState.PunchMovement;
        }
        else if (health >= 330 && health < 660)
        {
            newState = BossState.CutMovement;
        }
        else
        {
            newState = BossState.RandomMovement;
        }

        if (forceUpdate || currentState != newState)
        {
             currentState = newState;
            if (currentBehaviorCoroutine != null)
            {
                StopCoroutine(currentBehaviorCoroutine);
            }

            switch (currentState)
            {
                case BossState.RandomMovement:
                    currentBehaviorCoroutine = StartCoroutine(RandomMovement());
                    break;
                case BossState.CutMovement:
                    currentBehaviorCoroutine = StartCoroutine(CutMovement());
                    break;
                case BossState.PunchMovement:
                    currentBehaviorCoroutine = StartCoroutine(PunchMovement());
                    break;
            }
        }
    }

    void StartBehaviorBasedOnState()
    {
        switch (currentState)
        {
            case BossState.RandomMovement:
                currentBehaviorCoroutine = StartCoroutine(RandomMovement());
                break;
            case BossState.CutMovement:
                currentBehaviorCoroutine = StartCoroutine(CutMovement());
                break;
            case BossState.PunchMovement:
                currentBehaviorCoroutine = StartCoroutine(PunchMovement());
                break;
        }
    }

    void ChangeState(BossState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
            if (currentBehaviorCoroutine != null)
            {
                StopCoroutine(currentBehaviorCoroutine);
            }

            switch (currentState)
            {
                case BossState.RandomMovement:
                    currentBehaviorCoroutine = StartCoroutine(RandomMovement());
                    break;
                case BossState.CutMovement:
                    currentBehaviorCoroutine = StartCoroutine(CutMovement());
                    break;
                case BossState.PunchMovement:
                    currentBehaviorCoroutine = StartCoroutine(PunchMovement());
                    break;
            }
        }
    }

    IEnumerator PunchMovement()
    {
        Debug.Log("Iniciando PunchMovement");

        while (currentState == BossState.PunchMovement)
        {
            Debug.Log("Elegir punto de inicio en PunchMovement");
            Vector3 startPoint = ChooseRandomPoint();
            yield return StartCoroutine(MoveToPosition(startPoint));

            Debug.Log("Observar posición del jugador en PunchMovement");
            Vector3 playerPosition = player.transform.position;

            Debug.Log("Cambio de color a amarillo en PunchMovement");
            StartCoroutine(ChangeColorTemporary(Color.yellow, 0.5f));
            yield return new WaitForSeconds(0.5f);

            Debug.Log("Movimiento hacia el jugador en PunchMovement");
            spriteRenderer.color = Color.red;
            yield return StartCoroutine(MoveToPosition(playerPosition));

            Debug.Log("Regresar a punto aleatorio en PunchMovement");
            spriteRenderer.color = Color.white;
            Vector3 returnPoint = ChooseRandomPoint();
            yield return StartCoroutine(MoveToPosition(returnPoint));

            Debug.Log("Fin del ciclo PunchMovement");
            yield return new WaitForSeconds(1f);
        }
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
        while (currentState == BossState.RandomMovement)
        {
            // Detener el tremble effect antes de moverse
            StopTrembleEffect();

            Vector3 targetPosition = corners[random.Next(corners.Length)];
            yield return StartCoroutine(MoveToPosition(targetPosition));

            // Iniciar el tremble effect inmediatamente después de llegar a la esquina
            StartTrembleEffect();

            // Disparo
            yield return StartCoroutine(ShootRandomly());

            // No es necesario iniciar el tremble effect aquí ya que se inicia
            // inmediatamente después de moverse a la esquina
        }
    }

    IEnumerator MoveToPositionR(Vector3 targetPosition)
    {
        // Reutiliza la lógica de movimiento de RandomMovement
        float timeElapsed = 0;
        Vector3 startPosition = transform.position;

        while (timeElapsed < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed / moveDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }
    void StartTrembleEffect()
    {
        if (trembleCoroutine != null)
        {
            StopCoroutine(trembleCoroutine);
        }
        trembleCoroutine = StartCoroutine(TrembleEffect());
    }

    void StopTrembleEffect()
    {
        if (trembleCoroutine != null)
        {
            StopCoroutine(trembleCoroutine);
            trembleCoroutine = null;
        }
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
            
            // Detener el tremble effect justo antes de disparar
            StopTrembleEffect();

            // Seleccionar un patrón de disparo aleatorio y disparar
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
                    StartCoroutine(FireStraightAtPlayer());
                    break;
            }

            yield return new WaitForSeconds(shootDuration / shots);

            // Reiniciar el tremble effect inmediatamente después de disparar
            StartTrembleEffect();
        }
    }

    void FireInAllDirections()
    {
        int bulletAmount = 30;
        float angleStep = 360f / bulletAmount;
        float angle = 0f;

        for (int i = 0; i < bulletAmount; i++)
        {
            StopTrembleEffect();

            // Calcula la dirección de este disparo basándose en el ángulo
            Quaternion bulletRotation = Quaternion.Euler(0f, 0f, angle);
            FireBulletInDirection(bulletRotation);

            // Incrementa el ángulo para el próximo disparo
            angle += angleStep;
            StartTrembleEffect();
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

    IEnumerator FireStraightAtPlayer()
    {
        int numberOfShots = random.Next(3, 9); // Genera un número aleatorio entre 3 y 8
        Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;

        for (int i = 0; i < numberOfShots; i++)
        {
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            Quaternion bulletRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            FireBulletInDirection(bulletRotation);

            yield return new WaitForSeconds(0.1f); // Retraso entre disparos
        }
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

    Vector2 GetDirectionToPlayer()
    {
        // Asumimos que 'player' es una referencia válida al GameObject del jugador.
        return (player.transform.position - transform.position).normalized;
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

    bool IsAtCorner(Vector3 position)
    {
        foreach (var corner in corners)
        {
            if (Vector3.Distance(position, corner) < 0.1f) // Usa un valor pequeño para tolerancia
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator TrembleEffect()
    {
        float trembleDuration = 0.5f; // Duración del efecto de flotación
        float elapsedTime = 0f;

        Vector3 originalPosition = transform.position;
        float frequency = 5f; // Frecuencia del movimiento de flotación
        float amplitude = 0.05f; // Amplitud del movimiento de flotación

        while (elapsedTime < trembleDuration)
        {
            // Calcula un valor de desplazamiento basado en una función sinusoidal
            float displacement = Mathf.Sin(Time.time * frequency) * amplitude;

            // Aplica el desplazamiento a la posición original para crear un efecto de flotación
            transform.position = originalPosition + new Vector3(displacement, displacement, 0f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition; // Restaura la posición original al final
    }

     private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 8:     // PlayerBullets layer
                // The enemy is hit by a bullet
                health -= collision.gameObject.GetComponent<BaseAmmo>().GetDamageDealt();
                if (health <= 0)
                {
                    // The enemy dies
                    Destroy(gameObject);
                }
                break;
            default:
                break;
        }
    }

}