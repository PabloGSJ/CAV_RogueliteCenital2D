using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossMovement2 : MonoBehaviour
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
    public int health = 0;  // Agrega un valor de vida

    private enum BossState { RandomMovement, OtherState1, OtherState2 } // Agrega estados de comportamiento
    private BossState currentState;
    private bool isMoving;
    private Coroutine currentBehaviorCoroutine;
    private Coroutine trembleCoroutine;
    private Vector3[] edgeMidpoints;


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

    IEnumerator OtherState1Behavior()
    {
        while (currentState == BossState.OtherState1)
        {
            // Elige un punto de partida y un punto de destino
            Vector3 startPoint = ChooseRandomPoint();
            Vector3 endPoint = ChooseOppositePoint(startPoint);

            // Mueve al jefe al punto de partida
            yield return StartCoroutine(MoveToPosition(startPoint));

            // Mueve al jefe al punto de destino con un patrón (línea recta o seno)
            yield return StartCoroutine(MoveToPosition(endPoint));
            
            yield return new WaitForSeconds(1f); // Espera antes de reiniciar el comportamiento
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

    void SetInitialBossState()
    {
        if (health > 66)
        {
            currentState = BossState.RandomMovement;
            currentBehaviorCoroutine = StartCoroutine(RandomMovement());
        }
        else if (health > 33)
        {
            currentState = BossState.OtherState1;
            // Inicia el comportamiento correspondiente a OtherState1
        }
        else
        {
            currentState = BossState.OtherState2;
            // Inicia el comportamiento correspondiente a OtherState2
        }
    }

    void UpdateBossState(bool forceUpdate = false)
    {
        BossState newState;
        if (health > 66)
        {
            newState = BossState.RandomMovement;
        }
        else if (health > 33)
        {
            newState = BossState.OtherState1;
        }
        else
        {
            newState = BossState.OtherState2;
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
                case BossState.OtherState1:
                    currentBehaviorCoroutine = StartCoroutine(OtherState1Behavior());
                    break;
                case BossState.OtherState2:
                    // Inicia el comportamiento para OtherState2
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
            case BossState.OtherState1:
                // Inicia el comportamiento para OtherState1
                break;
            case BossState.OtherState2:
                // Inicia el comportamiento para OtherState2
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
                case BossState.OtherState1:
                    // Comienza el comportamiento para OtherState1
                    break;
                case BossState.OtherState2:
                    // Comienza el comportamiento para OtherState2
                    break;
            }
        }
    }

    void Update()
    {
        // Actualizaciones regulares, como ReflectSpriteBasedOnPosition
        ReflectSpriteBasedOnPosition();
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

}