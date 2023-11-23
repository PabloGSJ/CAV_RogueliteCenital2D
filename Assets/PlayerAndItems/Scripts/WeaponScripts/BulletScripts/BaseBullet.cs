using UnityEngine;

public abstract class BaseBullet : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log(gameObject.GetComponent<Rigidbody2D>().velocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
