using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemWeapon : BaseShopItem
{
    public GameObject[] possibleWeapons;

    protected override GameObject DecideShopItem()
    {
        GameObject chosenWeapon;
        chosenWeapon = possibleWeapons[Random.Range(0, possibleWeapons.Length)];

        return chosenWeapon;
    }


    protected override void SellItem(PlayerStateMachine player)
    {
        BaseWeapon weapon = _soldItem.GetComponent<BaseWeapon>();

        player.PickupWeapon(weapon);
        mycoll.enabled = false;     // disable the shopping stand
    }
}
