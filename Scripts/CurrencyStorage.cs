using System;
using UnityEngine;

public class CurrencyStorage : MonoBehaviour
{
    public static CurrencyStorage Instance;
    
    public int GetCurrency => currency;

    [SerializeField] private CurrencyUI currencyUI;
    [SerializeField] private int currency;

    private void Awake()
    {
        Instance = this;
    }

    public bool TrySpendAmount(int amount)
    {
        if (currency >= amount)
        {
            currency -= amount;
            currencyUI.UpdateText();
            return true;
        }

        return false;
    }
    
    public void AddCurrency(int amount)
    {
        currency += amount;
        currencyUI.UpdateText();
    }
}
