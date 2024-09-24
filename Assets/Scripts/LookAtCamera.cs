using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    enum CameraMode
    {
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted
    }

    [SerializeField] private CameraMode cameraMode = CameraMode.LookAt;

    public void LateUpdate()
    {
        switch (cameraMode)
        {
            case CameraMode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case CameraMode.LookAtInverted:
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;
            case CameraMode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case CameraMode.CameraForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }
}
