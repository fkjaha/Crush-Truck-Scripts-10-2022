using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraZoomer : MonoBehaviour
{
    public static CameraZoomer Instance;
    
    [SerializeField] private CinemachineVirtualCamera cameraToZoom;
    [SerializeField] private float zoomSpeed;

    private Vector3 _standardCameraOffset;
    private CinemachineTransposer _cameraToZoomTransporter;

    private void Awake()
    {
        Instance = this;
        
        _cameraToZoomTransporter = cameraToZoom.GetCinemachineComponent<CinemachineTransposer>();
        _standardCameraOffset = _cameraToZoomTransporter.m_FollowOffset;
    }

    public void SetCameraZoom(float zoomAmount)
    {
        StopAllCoroutines();
        StartCoroutine(ZoomCamera(zoomAmount));
    }

    private IEnumerator ZoomCamera(float zoomAmount)
    {
        Debug.Log(zoomAmount, this);
        Vector3 targetOffset = zoomAmount * _standardCameraOffset;
        while ((_cameraToZoomTransporter.m_FollowOffset - targetOffset).magnitude > .0001f)
        {
            _cameraToZoomTransporter.m_FollowOffset = Vector3.Lerp(_cameraToZoomTransporter.m_FollowOffset,
                targetOffset, zoomSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            Debug.Log("Offset: " +            targetOffset + " | " + zoomAmount);
        }
        Debug.Log("Camera zoom finished!");
    }
}
