using System;
using UnityEngine;

public class PlayerTargetCounter : MonoBehaviour
{
    [SerializeField] private TargetPointer targetPointer;

    [SerializeField] private Transform upgradeZoneTransform;
    [SerializeField] private Transform exchangeZoneTransform;
    [SerializeField] private GameObject collectTextGameObject;
    [SerializeField] private GameObject filledTextGameObject;
    [SerializeField] private Transform shreddedParent;

    private Transform GetExchangeZone
    {
        get
        {
            if (exchangeZoneTransform == null)
            {
                exchangeZoneTransform = ExchangeZone.Instance.gameObject.transform;
            }

            return exchangeZoneTransform;
        }
    }
    
    private Transform GetUpgradeZone
    {
        get
        {
            if (upgradeZoneTransform == null)
            {
                upgradeZoneTransform = UpgradeZone.Instance.gameObject.transform;
            }

            return upgradeZoneTransform;
        }
    }

    private void Update()
    {
        if (!Harvester.Instance.HarvestEnabled && ShreddedStorage.Instance.IsFilled)
        {
            SetPointerTarget(GetExchangeZone);
        }
        else
        {
            DisablePointer();
        }

        bool collectState = !ShreddedStorage.Instance.IsFilled && !Harvester.Instance.HarvestEnabled;
        
        // Debug.Log(collectState);
        
        if (collectTextGameObject.activeSelf != collectState)
        {
            collectTextGameObject.SetActive(collectState && shreddedParent.childCount > 0);
        }
        
        if(filledTextGameObject.activeSelf != ShreddedStorage.Instance.IsFilled)
            filledTextGameObject.SetActive(ShreddedStorage.Instance.IsFilled);
    }

    private void SetPointerTarget(Transform target)
    {
        if(!targetPointer.gameObject.activeSelf) targetPointer.gameObject.SetActive(true);
        targetPointer.SetNewTarget(target);
    }

    private void DisablePointer()
    {
        if(targetPointer.gameObject.activeSelf) targetPointer.gameObject.SetActive(false);
    }
}
