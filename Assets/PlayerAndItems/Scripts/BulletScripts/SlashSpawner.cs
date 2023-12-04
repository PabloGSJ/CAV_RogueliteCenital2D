using UnityEngine;

public class SlashSpawnerScript : MonoBehaviour
{
    private GameObject empty;

    private void Awake()
    {
        // preserve the bullets absolute proportions
        empty = GameObject.FindGameObjectWithTag("Empty");
        if (empty != null)
            Debug.Log(empty);
    }

    public void spawnSlash(GameObject slash, float dmgMod, Quaternion weaponRotation)
    {
        GameObject go = Instantiate(slash,
                                    new Vector3(transform.position.x,
                                                transform.position.y,
                                                transform.position.z),
                                    weaponRotation,
                                    empty.transform) as GameObject;
        go.transform.parent = empty.transform;
        go.GetComponent<BaseSlash>().DamageModifier = dmgMod;
    }
}
