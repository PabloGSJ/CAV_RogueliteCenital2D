using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemWeapon : BaseShopItem
{
    protected override void SellItem(PlayerStateMachine player)
    {
        BaseWeapon weapon = _soldItem.GetComponent<BaseWeapon>();

        player.PickupWeapon(weapon);
        mycoll.enabled = false;     // disable the shopping stand
    }
}
