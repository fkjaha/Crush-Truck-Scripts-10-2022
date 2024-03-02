using System;
using System.Collections.Generic;
using UnityEngine;

public class ShreddedStorage : Upgradable<int>
{
    public static ShreddedStorage Instance;

    public int Capacity => capacity;
    public bool IsFilled => _filled;
    public int GetContentCount => _allContent.Count;
    public Shredded DequeueShreddedContent
    {
        get
        {
            Shredded dequeued = _allContent.Dequeue();
            UpdateFilledState();
            return dequeued;
        }
    }

    [SerializeField] private int capacity;
    [SerializeField] private Transform storePositionTransform;
    
    private readonly Queue<Shredded> _allContent = new();
    private bool _filled;

    private void Awake()
    {
        Instance = this;
        UpdateFilledState();
    }

    public void TryCountContent(Shredded content)
    {
        if (!_filled)
        {
            _allContent.Enqueue(content);
            StoreContent(content);
            UpdateFilledState();
        }
    }

    private void StoreContent(Shredded content)
    {
        content.GetCollider.enabled = false;
        content.gameObject.SetActive(false);

        Transform contentTransform = content.transform;
        contentTransform.parent = storePositionTransform;
        contentTransform.localPosition = Vector3.zero;
    }

    private void UpdateFilledState()
    {
        _filled = _allContent.Count >= capacity;
    }

    public override void ApplyUpgradedValue()
    {
        capacity = isChangingOnConstant ? startValue + _currentLevel * constantValueDelta : allLevelValues[_currentLevel];
        Harvester.Instance.SyncHarvestLimit(this);
    }
}
