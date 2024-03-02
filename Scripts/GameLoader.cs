using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    public static GameLoader Instance; 
    
    [SerializeField] private List<LevelManager> levelsPrefabs;
    [SerializeField] private Transform levelsParent;
    [SerializeField] private UnityEvent onWinEvent;
    private const string SaveKey = "Level";

    private int _currentLevelIndex;
    private GameObject _currentSpawnedLevel;

    private void Awake()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _currentLevelIndex = PlayerPrefs.GetInt(SaveKey, 0);
        LoadLevel(_currentLevelIndex);
    }
    
    public void OnLevelPassed()
    {
        if(_currentLevelIndex < levelsPrefabs.Count-1) _currentLevelIndex++;
        SaveLevelProgress();
        onWinEvent.Invoke();
    }

    private void LoadLevel(int index)
    {
        LevelManager spawnedLevel = Instantiate(levelsPrefabs[_currentLevelIndex], levelsParent);
        spawnedLevel.PublicLevelNumber = _currentLevelIndex;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void DeleteCurrentLevel()
    {
        if(_currentSpawnedLevel != null)
            Destroy(_currentSpawnedLevel);
    }

    private void SaveLevelProgress()
    {
        PlayerPrefs.SetInt(SaveKey, _currentLevelIndex);
    }
}
