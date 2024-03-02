using UnityEngine;

public class FixedUpdateObjectFollower : ObjectFollower
{
    private void FixedUpdate()
    {
        Follow();
    }
    
    private protected override void Follow()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position + offset, speed * Time.fixedDeltaTime);
    }
}
