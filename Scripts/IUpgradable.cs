using UnityEngine;

public abstract class Upgradable: MonoBehaviour
{
    public abstract int GetCurrentLevel();
    
    public abstract int GetNumberOfLevels();

    public abstract void ApplyUpgradedValue();
    
    public abstract void UpgradeCurrentLevelByOne();
    
    public abstract void Save();
    public abstract void Load();
}
