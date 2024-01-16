using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseShopItem : MonoBehaviour
{
    // CONTEXT:

    public Collider2D mycoll;
    public SpriteRenderer sr;

    // layer variables
    private const int PlayerLayer = 6;

    // sell variables
    public GameObject Item;
    protected GameObject _soldItem;
    public int Price;


    // MONO BEHAVIOUR FUNCTIONS:

    // setup shop item
    private void Awake()
    {
        _soldItem = Instantiate(Item,
                                new Vector3(this.transform.position.x,
                                            this.transform.position.y,
                                            this.transform.position.z),
                                this.transform.rotation,
                                this.transform);

        // leave only the shop collider active
        Collider2D collider = _soldItem.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }
    }


    // AUXILIARY FUNCTIONS DECLARATIONS:

    // Ask if the item can be bought with the coins provided
    public bool TryBuy(int coins)
    {
        return coins >= Price;
    }

    // give the item to the player
    public void BuyItem(PlayerStateMachine player)
    {
        SellItem(player);
        player.Coins -= Price;
        player.UpdateConsumables();
    }

    // use the item on the player (buyer)
    protected abstract void SellItem(PlayerStateMachine player);
}