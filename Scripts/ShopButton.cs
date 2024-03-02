using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public Button GetButton => button;
    
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private List<Image> levelIndicators;

    [Header("Indicators Colors")] 
    [SerializeField] private Color disabledIndicatorColor;
    [SerializeField] private Color activeIndicatorColor;

    public void UpdateView(int cost, int level)
    {
        costText.text = cost == int.MaxValue ? "MAX" : "" + cost;
        levelText.text = "LVL " + level;
        
        for (int i = 0; i < levelIndicators.Count; i++)
        {
            levelIndicators[i].color =
                i <= level % levelIndicators.Count ? activeIndicatorColor : disabledIndicatorColor;
        }
    }
}
