using UnityEngine;

public class TruckVisuals : MonoBehaviour
{
    [SerializeField] private ParticleSystem shreddingParticleSystem;

    private void Update()
    {
        if (HarvestShredder.Instance.IsShreddingRightNow)
        {
            if(!shreddingParticleSystem.isEmitting)
                shreddingParticleSystem.Play();
        }
    }
}
