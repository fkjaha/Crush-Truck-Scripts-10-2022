using System;
using Cinemachine;
using UnityEngine;

public class Harvester : Upgradable<float>
{
    public static Harvester Instance;
    public bool HarvestEnabled => _harvestEnabled;

    [SerializeField] private Transform harvesterTransformToScale;
    [SerializeField] private bool capacityLimitEnabled;
    [SerializeField] private bool mainHarvester;

    private int _maxPassableMass;
    private bool _harvestEnabled;
    private int _maxHarvestBeforeStop;
    private int _numberOfHarvested;

    private void Awake()
    {
        Instance = this;
    }

    private protected override void Start()
    {
        if(mainHarvester)
            base.Start();
    }

    private void OnTriggerEnter(Collider other)
    {
        UpdateHarvestingState();
        if (other.TryGetComponent(out Harvestable harvestable))
        {
            if(harvestable is Rubbish && (!_harvestEnabled && capacityLimitEnabled)) return;
            if(!harvestable.IsHarvestable) return;
            // if(harvestable.GetMass > _maxPassableMass) return;
            Harvest(harvestable);
        }
    }

    private void Harvest(Harvestable harvestable)
    {
        harvestable.OnHarvested();
    }

    public override void ApplyUpgradedValue()
    {
        // _maxPassableMass = allLevelValues[_currentLevel];
        harvesterTransformToScale.localScale = Vector3.one * (isChangingOnConstant ? startValue + _currentLevel * constantValueDelta : allLevelValues[_currentLevel]);
        CameraZoomer.Instance.SetCameraZoom(isChangingOnConstant
            ? startValue + _currentLevel * constantValueDelta
            : allLevelValues[_currentLevel]);
    }

    public void SyncHarvestLimit(ShreddedStorage shreddedStorage)
    {
        _maxHarvestBeforeStop = shreddedStorage.Capacity;
    }
    
    public void CountSellingHarvest()
    {
        _numberOfHarvested--;
        UpdateHarvestingState();
    }
    
    public void CountPickingHarvest()
    {
        _numberOfHarvested++;
        UpdateHarvestingState();
    }

    private void UpdateHarvestingState()
    {
        _harvestEnabled = _numberOfHarvested < _maxHarvestBeforeStop;
    }
}
