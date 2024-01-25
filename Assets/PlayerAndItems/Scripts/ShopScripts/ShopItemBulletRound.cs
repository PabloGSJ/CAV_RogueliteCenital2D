using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemBulletRound : BaseShopItem
{
    protected override GameObject DecideShopItem()
    {
        return Item;
    }

    protected override void SellItem(PlayerStateMachine player)
    {
        BulletRound bulletRound = _soldItem.GetComponent<BulletRound>();

        bulletRound.UseConsumable(player);
        Destroy(_soldItem);
        mycoll.enabled = false;     // disable the shopping stand
    }
}
