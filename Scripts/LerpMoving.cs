using System;
using UnityEngine;

public class LerpMoving : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 pointOffsetA;
    [SerializeField] private Vector3 pointOffsetB;

    private Vector3 _pointA;
    private Vector3 _pointB;

    private Vector3 _targetPoint;

    private void Start()
    {
        _pointA = transform.position + pointOffsetA;
        _pointB = transform.position + pointOffsetB;

        _targetPoint = _pointA;
    }

    private void Update()
    {
        if (transform.position != _targetPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPoint, speed * Time.deltaTime);
        }
        else
        {
            _targetPoint = _targetPoint == _pointA ? _pointB : _pointA;
        }
    }
}
