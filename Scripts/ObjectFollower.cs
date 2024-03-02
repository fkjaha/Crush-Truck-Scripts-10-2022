using System;
using UnityEngine;

public class ObjectFollower : MonoBehaviour
{
    [SerializeField] private protected Transform target;
    [SerializeField] private protected float speed;
    [SerializeField] private protected Vector3 offset;

    private protected virtual void Follow()
    {
        
    }

    private void OnEnable()
    {
        transform.position = target.position + offset;
    }
}
