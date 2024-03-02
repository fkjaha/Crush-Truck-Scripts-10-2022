using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelsManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _models;
    [SerializeField] private List<GameObject> _models1;

    private GameObject _currentModel;
    private int _modelIndex;

    private void Start()
    {
        UpdadeView();
    }

    public void UpgradeModelByOne()
    {
        // if(_modelIndex < _models.Count - 1)
            _modelIndex++;
    }

    public void UpdadeView()
    {
        for (int i = 0; i < _models.Count; i++)
        {
            _models[i].SetActive(_modelIndex%_models.Count == i);
            _models1[i].SetActive(_modelIndex%_models.Count == i);
        }
    }
}
