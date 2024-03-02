using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    
    public int PublicLevelNumber;
    public float GetLevelProgression => 1 - levelRubbish?.Count/(float)_startRubbishCount ?? 1f;

    [SerializeField] private List<Rubbish> levelRubbish = new();

    private int _startRubbishCount;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _startRubbishCount = levelRubbish.Count;
        LevelProgressionUI.Instance.UpdateLevelUI();
    }

    public void CountRubbishRecycling(Rubbish rubbish)
    {
        levelRubbish.Remove(rubbish);
        LevelProgressionUI.Instance.UpdateLevelUI();
        CheckLevelProgress();
    }

    private void CheckLevelProgress()
    {
        if (levelRubbish == null || levelRubbish.Count == 0)
        {
            GameLoader.Instance.OnLevelPassed();
        }
    }

    [ContextMenu("Find Rubbish On Scene")]
    private void FindRubbishOnScene()
    {
        levelRubbish = FindObjectsOfType<Rubbish>().ToList();
    }
}
