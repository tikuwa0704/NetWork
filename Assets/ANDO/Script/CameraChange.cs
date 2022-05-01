using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraChange : MonoBehaviour
{
    
    CinemachineVirtualCamera virtualCamera;

    public void ChangeLookAt(Transform lookAtTarget)
    {
        TryGetComponent(out virtualCamera);

        virtualCamera.Follow = lookAtTarget;

    }
}
