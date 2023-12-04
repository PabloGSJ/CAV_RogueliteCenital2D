using UnityEngine;

public class SlashSpawnerScript : MonoBehaviour
{
    public float AttackRange;

    public void spawnSlash(GameObject slash, float dmgMod)
    {
        GameObject go = Instantiate(slash,
                                    new Vector3(transform.position.x,
                                                transform.position.y,
                                                transform.position.z),
                                    Quaternion.identity,
                                    gameObject.transform) as GameObject;
        go.transform.parent = gameObject.transform;
        go.GetComponent<BaseSlash>().DamageModifier = dmgMod;
    }
}
