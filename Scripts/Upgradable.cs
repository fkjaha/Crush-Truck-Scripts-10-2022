using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Upgradable<T> : Upgradable
{
    [Header("Upgradable")]
    [SerializeField] private protected List<T> allLevelValues;
    [SerializeField] private protected UnityEvent onLevelUpgraded;

    [SerializeField] private protected bool isChangingOnConstant;
    [SerializeField] private protected T constantValueDelta;
    [SerializeField] private protected T startValue;

    [Space(20f)]
    
    [SerializeField] private string saveKey;
    
    private protected int _currentLevel;

    private protected virtual void Start()
    {
        Load();
        ApplyUpgradedValue();
    }

    public override int GetCurrentLevel()
    {
        return _currentLevel;
    }

    public override int GetNumberOfLevels()
    {
        return isChangingOnConstant ? int.MaxValue - 1 : allLevelValues.Count;
    }

    public abstract override void ApplyUpgradedValue();

    public override void UpgradeCurrentLevelByOne()
    {
        _currentLevel++;
        onLevelUpgraded.Invoke();
    }

    public override void Save()
    {
        PlayerPrefs.SetInt(saveKey, _currentLevel);
        Debug.Log("Saved");
    }
    
    public override void Load()
    {
        _currentLevel = PlayerPrefs.GetInt(saveKey, 0);
        Debug.Log("Loaded");
    }
}
