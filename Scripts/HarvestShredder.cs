using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class HarvestShredder : MonoBehaviour
{
    public static HarvestShredder Instance;

    public bool IsShreddingRightNow => _shredding;

    [SerializeField] private float objectsFlyTime;
    [SerializeField] private float objectsFlyForce;
    
    [Space(20f)]
    [SerializeField] private float timeBetweenItemsShredding;
    [SerializeField] private float throwForce;
    [SerializeField] private Transform targetCatchPositionTransform;
    [SerializeField] private Transform targetEjectPositionTransform;
    [SerializeField] private Transform shreddedParent;
    [SerializeField] private int ejectDirMaxOffset;

    private readonly Queue<Rubbish> _allHarvest = new();
    private bool _shredding;

    private void Start()
    {
        Instance = this;
        StartCoroutine(ShredHarvest());
    }

    public void CatchHarvest(Rubbish rubbish)
    {
        rubbish.transform.parent = targetCatchPositionTransform;
        rubbish.transform.DOLocalRotate(new Vector3(0f, Random.Range(0, 360), 0f), objectsFlyTime);
        rubbish.transform.DOScale(Vector3.zero, objectsFlyTime).SetEase(Ease.InCirc);
        rubbish.transform.DOLocalJump(Vector3.zero, objectsFlyForce * Random.Range(1f, 2f), 1,
            objectsFlyTime).OnComplete(() => CountHarvest(rubbish));
    }

    private void CountHarvest(Rubbish rubbish)
    {
        _allHarvest.Enqueue(rubbish);
        rubbish.gameObject.SetActive(false);
        LevelManager.Instance.CountRubbishRecycling(rubbish);
    }

    private void SpawnAndEjectShreddedRubbish(Rubbish rubbish)
    {
        Harvestable shreddedRubbish = Instantiate(rubbish.GetShreddedPrefab, targetEjectPositionTransform.position, Quaternion.identity, shreddedParent);

        Vector3 ejectDirection = Quaternion.AngleAxis(Random.Range(-ejectDirMaxOffset, ejectDirMaxOffset),
                                     targetEjectPositionTransform.up) * targetEjectPositionTransform.right;
        shreddedRubbish.GetRigidbody.AddForce(ejectDirection * throwForce, ForceMode.Impulse);
    }

    private void ShredSingleRubbish()
    {
        Rubbish rubbish = _allHarvest.Dequeue();
        rubbish.transform.parent = null;
        SpawnAndEjectShreddedRubbish(rubbish);
        Destroy(rubbish.gameObject);
    }
    
    private IEnumerator ShredSingleRubbishCor()
    {
        Rubbish rubbish = _allHarvest.Dequeue();
        for (int i = 0; i < rubbish.GetNumberOfPrefabs; i++)
        {
            SpawnAndEjectShreddedRubbish(rubbish);
            yield return new WaitForSeconds(timeBetweenItemsShredding);
        }
        rubbish.transform.parent = null;
        Destroy(rubbish.gameObject);
    }

    private IEnumerator ShredHarvest()
    {
        while (true)
        {
            // Debug.Log(_allHarvest.Count);
            if (_allHarvest.Count > 0)
            {
                _shredding = true;
                // ShredSingleRubbish();
                yield return StartCoroutine(ShredSingleRubbishCor());
            }
            else
            {
                _shredding = false;
                yield return new WaitForSeconds(timeBetweenItemsShredding);
            }
        }
    }
}
