using UnityEngine;

public abstract class Harvestable: MonoBehaviour
{
    public int GetMass => mass;
    public int GetCost => cost;
    public Rigidbody GetRigidbody => harvestableRigidbody;
    public bool IsHarvestable => _harvestable;
    
    [SerializeField] protected int mass;
    [SerializeField] protected int cost;
    
    [Space(20f)]
    [SerializeField] protected Collider harvestCollider;
    [SerializeField] protected Rigidbody harvestableRigidbody;

    protected bool _harvestable = true;

    public abstract void EnableGravityForce();

    public abstract void OnHarvested();
}
