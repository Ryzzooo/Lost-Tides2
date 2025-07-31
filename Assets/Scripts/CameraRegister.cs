using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraRegister : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;

    private void Awake()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        CameraManager.Register(vcam);
    }

    private void OnDestroy()
    {
        CameraManager.Unregister(vcam);
    }
}
