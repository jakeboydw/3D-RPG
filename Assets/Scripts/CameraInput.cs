using UnityEngine;
using UnityEngine.InputSystem;

public class CameraInput : MonoBehaviour
{
    public CameraLook cameraLook;

    public void OnLook(InputValue value)
    {
        cameraLook.SetLookInput(value.Get<Vector2>());
    }
}
