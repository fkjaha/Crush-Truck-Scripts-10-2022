using UnityEngine;

public class Shredded : Harvestable
{
    public Collider GetCollider => harvestCollider;
    
    public override void EnableGravityForce()
    {
        if (harvestableRigidbody.isKinematic)
            harvestableRigidbody.isKinematic = false;
    }

    public override void OnHarvested()
    {
        ShreddedStorage.Instance.TryCountContent(this);
    }
}
