using System;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public List<ShopSlot> GetSlots => slots;

    [SerializeField] private CurrencyStorage currencyStorage;
    [SerializeField] private List<ShopSlot> slots;

    public int GetUpgradableLevel(int index)
    {
        return slots[index].GetUpgradable.GetCurrentLevel();
    }
    
    public int GetUpgradeCost(int index)
    {
        return slots[index].GetCostOnLevel(slots[index].GetUpgradable.GetCurrentLevel() + 1);
    }
    
    public void UpgradeUpgradable(int index)
    {
        Upgradable upgradable = slots[index].GetUpgradable;
        if (currencyStorage.TrySpendAmount(slots[index].GetCostOnLevel(upgradable.GetCurrentLevel() + 1)))
        {
            upgradable.UpgradeCurrentLevelByOne();
            upgradable.ApplyUpgradedValue();
        }
    }

    public void SaveShopProgress()
    {
        foreach (ShopSlot shopSlot in slots)
        {
            shopSlot.GetUpgradable.Save();
        }
    }
}

[Serializable]
public class ShopSlot
{
    public Upgradable GetUpgradable => upgradable;
    public ShopButton GetShopButton => upgradeShopButton;
    public int GetCostOnLevel (int index) => index > _maxLevel ? int.MaxValue : (costChangingOnConstant ?
        startCost + costDelta * upgradable.GetCurrentLevel() : levelCosts[index]);
    public int MaxLevel => _maxLevel;

    [SerializeField] private Upgradable upgradable;
    [SerializeField] private List<int> levelCosts;
    [SerializeField] private ShopButton upgradeShopButton;

    [SerializeField] private bool costChangingOnConstant;
    [SerializeField] private int startCost;
    [SerializeField] private int costDelta;

    private int _maxLevel;

    public void FindMaxLevel()
    {
        if (costChangingOnConstant && upgradable.GetNumberOfLevels() == int.MaxValue-1)
        {
            _maxLevel = int.MaxValue - 1;
        }
        else
        {
            _maxLevel = Math.Min(levelCosts.Count-1, upgradable.GetNumberOfLevels()-1);
        }
    }
}
