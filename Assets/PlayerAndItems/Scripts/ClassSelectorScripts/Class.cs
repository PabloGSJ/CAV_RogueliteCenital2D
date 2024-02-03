using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Class : MonoBehaviour
{
    public Collider2D mycoll;
    public ClassSelector cs;
    public Vector2 offset;

    // class item variables
    public GameObject Item;
    protected GameObject _classItem;


    // MONO BEHAVIOUR FUNCTIONS:

    // setup shop item
    private void Awake()
    {
        _classItem = Instantiate(Item,
                                new Vector3(this.transform.position.x + offset.x,
                                            this.transform.position.y + offset.y,
                                            this.transform.position.z),
                                this.transform.rotation,
                                this.transform);
        _classItem.GetComponent<BaseWeapon>().sr.sortingLayerName = "Hover";

        // leave only the class selector collider active
        _classItem.GetComponent<Collider2D>().enabled = false;
    }

    private void Update()
    {
        _classItem.transform.position = new Vector3(this.transform.position.x + offset.x,
                                                    this.transform.position.x + offset.y,
                                                    this.transform.position.z);
    }


    // AUXILIARY FUNCTIONS DECLARATIONS:

    // use the item on the player (buyer)
    public void SelectClass(PlayerStateMachine player)
    {
        BaseWeapon weapon = _classItem.GetComponent<BaseWeapon>();
        player.PickupWeapon(weapon);
        cs.ClassSelected();
    }
}
