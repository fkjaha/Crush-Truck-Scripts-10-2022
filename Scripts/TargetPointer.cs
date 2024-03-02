using UnityEngine;

public class TargetPointer : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;

    private void Update()
    {
        RotateTowardsTarget();
    }

    private void RotateTowardsTarget()
    {
        if(targetTransform == null) return;
        
        var transformPosition = transform.position;
        var targetTransformPosition = targetTransform.position;
        transform.rotation = Quaternion.LookRotation( new Vector3(targetTransformPosition.x, transformPosition.y, targetTransformPosition.z)
                                                      - transformPosition, Vector3.up);
    }

    public void SetNewTarget(Transform target)
    {
        targetTransform = target;
    }
}
