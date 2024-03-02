using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] private Vector3 rotationAxis;
    [SerializeField] private float speed;

    private void Update()
    {
        if(TruckController.Instance.GetVelocity != Vector3.zero) RotateWheel();
    }

    private void RotateWheel()
    {
        transform.Rotate(rotationAxis * speed * Time.deltaTime);
    }
}
