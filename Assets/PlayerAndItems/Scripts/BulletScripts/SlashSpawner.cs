using UnityEngine;

public class SlashSpawnerScript : MonoBehaviour
{
    public float AttackRange;

    public void spawnSlash(GameObject slash)
    {
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(this.transform.position, AttackRange);

        //GameObject go = Instantiate(slash,
        //                            new Vector3(transform.position.x,
        //                                        transform.position.y,
        //                                        transform.position.z),
        //                            Quaternion.identity,
        //                            gameObject.transform) as GameObject;
        // go.transform.parent = gameObject.transform;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, AttackRange);
    }
}
