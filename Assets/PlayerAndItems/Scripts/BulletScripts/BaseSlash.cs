using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSlash : BaseAmmo
{
    public float TTL = 0.3f;
    private GameObject _followGO;
    public GameObject FollowGO { set { _followGO = value; } }

    private void Update()
    {
        if (TTL <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            TTL -= Time.deltaTime;
            this.transform.position = new Vector3(_followGO.transform.position.x,
                                                  _followGO.transform.position.y,
                                                  _followGO.transform.position.z);
            this.transform.rotation = _followGO.transform.rotation;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy(gameObject);
    }
}
