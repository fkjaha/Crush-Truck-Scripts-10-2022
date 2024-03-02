using UnityEngine;

public class HarvestRigidbodyActivator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Rubbish harvestable))
        {
            if(!harvestable.IsHarvestable) return;
            harvestable.EnableGravityForce();
        }
    }
}
