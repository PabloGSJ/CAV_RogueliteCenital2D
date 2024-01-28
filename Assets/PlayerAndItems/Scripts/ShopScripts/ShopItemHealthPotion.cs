using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemHealthPotion : BaseShopItem
{
    protected override GameObject DecideShopItem()
    {
        return Item;
    }

    protected override void SellItem(PlayerStateMachine player)
    {
        HealthPotion healthPotion = _soldItem.GetComponent<HealthPotion>();

        healthPotion.UseConsumable(player);
        Destroy(_soldItem);
        mycoll.enabled = false;     // disable the shopping stand
    }
}
