using UnityEngine;

public class UpdateObjectFollower : ObjectFollower
{
    private void Update()
    {
        Follow();
    }

    private protected override void Follow()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position + offset, speed * Time.deltaTime);
    }
}
