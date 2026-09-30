using System;
using UnityEngine;


public class ShopUI : MonoBehaviour
{
    // [MODIF] Action<int>: without a parameter, it is impossible to know WHICH TowerShop.towers tower to buy.
    public Action<int> ValidateBuy;

    // [MODIF] + int towerIndex, connected to the OnClick event of each tower button.
    public void Validate(int towerIndex)
    {
        OnValidateBuy(towerIndex);
    }

    public void OnValidateBuy(int towerIndex)
    {
        ValidateBuy?.Invoke(towerIndex);
    }
}
