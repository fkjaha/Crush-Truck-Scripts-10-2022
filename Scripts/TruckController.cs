using System;
using UnityEngine;

public class TruckController : Upgradable<float>
{
    public static TruckController Instance;
    public Vector3 GetVelocity => truckRigidbody.velocity;
    
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private Rigidbody truckRigidbody;
    [SerializeField] private Joystick joystick;

    private Transform _truckTransform;
    private Vector2 _frameInput;

    private void Awake()
    {
        Instance = this;
    }

    private protected override void Start()
    {
        base.Start();
        _truckTransform = truckRigidbody.transform;
    }

    private void Update()
    {
        GetInput();
        MoveTruck();
        RotateTruck();
    }

    private void GetInput()
    {
        _frameInput = new Vector2(joystick.Horizontal, joystick.Vertical);
    }

    private void MoveTruck()
    {
        truckRigidbody.velocity = new Vector3(joystick.Vertical, 0f, -joystick.Horizontal) * speed;
        
    }

    private void RotateTruck()
    {
        if (_frameInput == Vector2.zero) return;

        Quaternion targetRotation =
            Quaternion.LookRotation(new Vector3(_frameInput.x, 0f, _frameInput.y).normalized, Vector3.up);
        _truckTransform.rotation =
            Quaternion.Lerp(_truckTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public override void ApplyUpgradedValue()
    {
        speed = isChangingOnConstant ? startValue + _currentLevel * constantValueDelta : allLevelValues[_currentLevel];
    }
}
