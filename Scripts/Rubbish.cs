using UnityEngine;

public class Rubbish : Harvestable
{
    public Harvestable GetShreddedPrefab => shreddedPrefab;
    public int GetNumberOfPrefabs => numberOfPrefabs;

    [SerializeField] private int numberOfPrefabs;
    [SerializeField] private Harvestable shreddedPrefab;

    #region Tests

    [ContextMenu("AutoFill")]
    public void EditorAutoFill()
    {
        harvestableRigidbody = GetComponent<Rigidbody>();
        harvestCollider = GetComponent<BoxCollider>();
    }

    #endregion
    
    public override void EnableGravityForce()
    {
        if (harvestableRigidbody.isKinematic)
            harvestableRigidbody.isKinematic = false;
    }

    public override void OnHarvested()
    {
        _harvestable = false;
        harvestCollider.enabled = false;
        harvestableRigidbody.isKinematic = true;
        Harvester.Instance.CountPickingHarvest();
        HarvestShredder.Instance.CatchHarvest(this);
    }
}
