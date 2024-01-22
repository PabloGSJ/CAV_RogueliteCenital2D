using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Class : MonoBehaviour
{
    public Collider2D mycoll;
    public ClassSelector cs;

    // class item variables
    public GameObject Item;
    protected GameObject _classItem;


    // MONO BEHAVIOUR FUNCTIONS:

    // setup shop item
    private void Awake()
    {
        _classItem = Instantiate(Item,
                                new Vector3(this.transform.position.x,
                                            this.transform.position.y,
                                            this.transform.position.z),
                                this.transform.rotation,
                                this.transform);

        // leave only the class selector collider active
        _classItem.GetComponent<Collider2D>().enabled = false;
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
