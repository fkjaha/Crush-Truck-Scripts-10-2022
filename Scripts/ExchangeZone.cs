using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ExchangeZone : MonoBehaviour
{
    public static ExchangeZone Instance;
    
    [SerializeField] private Transform itemsFinalTargetAndParent;
    [SerializeField] private float timePerItem;
    [SerializeField] private float harvestRadius;
    
    [Header("Visuals")]
    [SerializeField] private float objectsFlyTime;
    [SerializeField] private float objectsFlyForce;

    private ShreddedStorage _shreddedStorage;
    private Transform _harvestCarrierTransform;
    private bool _exchangeEnabled;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _shreddedStorage = ShreddedStorage.Instance;
        _harvestCarrierTransform = _shreddedStorage.transform;
        StartCoroutine(ExchangeHarvest());
    }

    private void Update()
    {
        _exchangeEnabled = Vector3.Distance(transform.position, _harvestCarrierTransform.position) <= harvestRadius;
    }

    private IEnumerator ExchangeHarvest()
    {
        while (true)
        {
            if (_exchangeEnabled && _shreddedStorage.GetContentCount > 0)
            {
                ExchangeSingleHarvest(_shreddedStorage.DequeueShreddedContent);
            }

            yield return new WaitForSeconds(timePerItem);
        }
    }

    private void ExchangeSingleHarvest(Shredded shredded)
    {
        shredded.gameObject.SetActive(true);
        Transform shreddedTransform = shredded.transform;
        
        shreddedTransform.parent = itemsFinalTargetAndParent;
        shreddedTransform.DOLocalJump(Vector3.zero, objectsFlyForce, 1, objectsFlyTime)
            .OnComplete(() =>
            {
                CurrencyStorage.Instance.AddCurrency(shredded.GetCost);
                TextPool.Instance.PlayString("+" +  shredded.GetCost);
                Destroy(shredded.gameObject);
                Harvester.Instance.CountSellingHarvest();
            });
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, harvestRadius);
    }
}
