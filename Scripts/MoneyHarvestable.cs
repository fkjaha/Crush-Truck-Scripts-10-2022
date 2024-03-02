using UnityEngine;
using UnityEngine.Events;

public class MoneyHarvestable : Harvestable
{
    [SerializeField] private UnityEvent onCollectedEvent;

    public override void EnableGravityForce()
    {
        if (harvestableRigidbody.isKinematic)
            harvestableRigidbody.isKinematic = false;
    }

    public override void OnHarvested()
    {
        CurrencyStorage.Instance.AddCurrency(GetCost);
        CoinsAnimator.Instance.AnimateSingleImage(CanvasPositionCalculator.Instance.GetScreenPositionVector2(transform.position));
        onCollectedEvent.Invoke();
        Destroy(gameObject);
    }
}
