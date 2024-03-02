using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIImagesPool : MonoBehaviour
{
    [SerializeField] private Transform imagesParent;
    [SerializeField] private Image imagePrefab;
    [SerializeField] private int poolSize;

    private Queue<Image> _pool;

    private void Start()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        _pool = new();
        for (int i = 0; i < poolSize; i++)
        {
            Image poolObject = Instantiate(imagePrefab, imagesParent);
            _pool.Enqueue(poolObject);
        }
    }

    public Image GetCoinImage()
    {
        Image dequeued = _pool.Dequeue();
        _pool.Enqueue(dequeued);
        return dequeued;
    }
}
