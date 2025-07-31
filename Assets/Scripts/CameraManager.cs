using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
 
public class CameraManager : MonoBehaviour
{
    static List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();
 
    public static CinemachineVirtualCamera ActiveCamera = null;
 
    public static bool IsActiveCamera(CinemachineVirtualCamera camera)
    {
        return camera == ActiveCamera;
    }
 
    public static void Register(CinemachineVirtualCamera camera)
{
    Debug.Log($"Register: {camera.name}");
    cameras.Add(camera);
}

public static void Unregister(CinemachineVirtualCamera camera)
{
    Debug.Log($"Unregister: {camera.name}");
    cameras.Remove(camera);
}

public static void SwitchCamera(CinemachineVirtualCamera newCamera)
{
    Debug.Log($"Switch to: {newCamera.name}");
    newCamera.Priority = 10;
    ActiveCamera = newCamera;

    foreach (var cam in cameras)
    {
        Debug.Log($"Checking cam: {cam.name}");
        if (cam != newCamera)
        {
            cam.Priority = 0;
        }
    }
}

}