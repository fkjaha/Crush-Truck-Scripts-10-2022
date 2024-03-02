using UnityEngine;

public class UpgradeZone : MonoBehaviour
{
    public static UpgradeZone Instance;
    
    [SerializeField] private float upgradeRadius;

    private ShreddedStorage _shreddedStorage;
    private Transform _harvestCarrierTransform;
    private bool _shopEnabled;

    private void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        _shreddedStorage = ShreddedStorage.Instance;
        _harvestCarrierTransform = _shreddedStorage.transform;
    }

    private void Update()
    {
        _shopEnabled = Vector3.Distance(transform.position, _harvestCarrierTransform.position) <= upgradeRadius;

        if (ShopUI.Instance.GetShopUIGameObject.activeSelf != _shopEnabled)
        {
            ShopUI.Instance.GetShopUIGameObject.SetActive(_shopEnabled);
            ShopUI.Instance.UpdateUI();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, upgradeRadius);
    }
}
