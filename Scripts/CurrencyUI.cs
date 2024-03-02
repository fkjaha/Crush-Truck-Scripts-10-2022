using DG.Tweening;
using TMPro;
using UnityEngine;

public class CurrencyUI : MonoBehaviour
{
    [SerializeField] private CurrencyStorage currencyStorage;
    [SerializeField] private TextMeshProUGUI text;

    private int _lastNumber;
    
    private void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        // text.text = "" + currencyStorage.GetCurrency;
        // text.DOText("" + currencyStorage.GetCurrency, 1f);
        text.DOCounter(_lastNumber, currencyStorage.GetCurrency, 1f);
        _lastNumber = currencyStorage.GetCurrency;
    }
}
