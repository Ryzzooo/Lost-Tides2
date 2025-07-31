using UnityEngine;
using Cinemachine;

public class SwitchCam : MonoBehaviour
{
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CameraManager.SwitchCamera(cam1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CameraManager.SwitchCamera(cam2);
        }
    }

}
