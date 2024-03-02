using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressionUI : MonoBehaviour
{
    public static LevelProgressionUI Instance;
    
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI levelText;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateLevelUI()
    {
        levelText.text = "" + LevelManager.Instance.PublicLevelNumber;
        fillImage.fillAmount = LevelManager.Instance.GetLevelProgression;
    }
}
