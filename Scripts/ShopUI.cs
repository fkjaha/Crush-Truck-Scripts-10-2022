using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance;
    
    public GameObject GetShopUIGameObject => shopUIGameObject;
    
    [SerializeField] private Shop shop;
    [SerializeField] private GameObject shopUIGameObject;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ConfigureShopUIElements();
    }

    private void ConfigureShopUIElements()
    {
        List<ShopSlot> slots = shop.GetSlots;
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].FindMaxLevel();
            AssignBuyButtonsClickEvent(slots[i].GetShopButton, i);
        }
    }

    private void AssignBuyButtonsClickEvent(ShopButton shopButton, int index)
    {
        shopButton.GetButton.onClick.AddListener(() =>
        {
            shop.UpgradeUpgradable(index);
            shopButton.UpdateView(shop.GetUpgradeCost(index), shop.GetUpgradableLevel(index));
        });
        shopButton.UpdateView(shop.GetUpgradeCost(index), shop.GetUpgradableLevel(index));
    }

    public void UpdateUI()
    {
        List<ShopSlot> slots = shop.GetSlots;
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].GetShopButton.UpdateView(shop.GetUpgradeCost(i), shop.GetUpgradableLevel(i));
        }
    }
    
    
}
