using UnityEngine;

public class SlashSpawnerScript : MonoBehaviour
{
    private GameObject _empty;
    private int _yscalemod;

    private void Awake()
    {
        // preserve the bullets absolute proportions
        _empty = GameObject.FindGameObjectWithTag("Empty");
        _yscalemod = 1;
    }

    public void spawnSlash(GameObject slash, float dmgMod, Quaternion weaponRotation)
    {
        GameObject go = Instantiate(slash,
                                    new Vector3(transform.position.x,
                                                transform.position.y,
                                                transform.position.z),
                                    weaponRotation,
                                    _empty.transform) as GameObject;
        go.transform.parent = _empty.transform;
        go.GetComponent<BaseSlash>().DamageModifier = dmgMod;
        go.GetComponent<BaseSlash>().FollowGO = this.gameObject;
        go.transform.localScale = new Vector3(go.transform.localScale.x,
                                              go.transform.localScale.y * _yscalemod,
                                              go.transform.localScale.z);
        _yscalemod *= -1;
    }
}
