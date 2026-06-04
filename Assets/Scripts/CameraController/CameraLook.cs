using Unity.Cinemachine;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public Transform player;

    public LockOnSystem lockOnSystem;

    public float sensitivity = 200f;

    public float maxPitch = 70f;
    public float minPitch = -40f;

    [Header("镜头锁定设置")]
    public float lockPitch = 20f;

    public float lockRotationSpeed = 10f;

    public float lockCenterWeight = 0.3f;
    public float maxLockOffset = 3f;
    public float lockMoveSmoothTime = 0.2f;

    [Header("相机距离设置")]
    public CinemachineThirdPersonFollow follow;

    public float minCameraDistance = 4f;
    public float maxCameraDistance = 8f;

    public float minTargetDistance = 1f;
    public float maxTargetDistance = 10f;

    public float cameraDistanceSmoothTime = 0.2f;

    public float normalDistance = 4.5f;

    private float yaw;
    private float pitch;
    private Vector2 lookInput;

    private float yawVelocity;
    private float pitchVelocity;

    private Vector3 moveVelocity;

    private float distanceVelocity;

    public void SetLookInput(Vector2 input)
    {
        lookInput = input;
    }

    private void LateUpdate()
    {
        UpdatePosition();
        UpdateRotation();
        UpdateCameraDistance();
    }

    private void UpdateRotation()
    {
        if (lockOnSystem != null && lockOnSystem.IsLocked)
        {
            LockLook();
        }
        else
        {
            FreeLook();
        }
    }

    private void FreeLook()
    {
        yaw += lookInput.x * sensitivity * Time.deltaTime;

        pitch -= lookInput.y * sensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0);
    }

    private void LockLook()
    {
        Transform target = lockOnSystem.CurrentTarget;

        if (target == null) return;

        Vector3 dir = target.position - transform.position;
        
        float targetYaw = Quaternion.LookRotation(dir).eulerAngles.y;

        yaw = Mathf.SmoothDampAngle(yaw, targetYaw, ref yawVelocity, 1f / lockRotationSpeed);
        pitch = Mathf.SmoothDampAngle(pitch, lockPitch, ref pitchVelocity, 1f / lockRotationSpeed);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0);
    }

    private void UpdatePosition()
    {
        Vector3 targetPosition;

        if (lockOnSystem != null && lockOnSystem.IsLocked)
        {
            Transform target = lockOnSystem.CurrentTarget;
            Vector3 offset = (target.position - player.position) * lockCenterWeight;
            offset = Vector3.ClampMagnitude(offset, maxLockOffset);
            targetPosition = player.position + offset;
        }
        else
        {
            targetPosition = player.position;
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref moveVelocity, lockMoveSmoothTime);
    }

    private void UpdateCameraDistance()
    {
        if (follow == null) return;

        float targetDistance = normalDistance;

        if (lockOnSystem != null && lockOnSystem.IsLocked)
        {
            float distance = Vector3.Distance(player.position, lockOnSystem.CurrentTarget.position);
            
            //根据敌我距离动态调整CameraDistance
            float t = Mathf.InverseLerp(minTargetDistance, maxTargetDistance, distance);
            targetDistance = Mathf.Lerp(minCameraDistance, maxCameraDistance, t);
        }

        follow.CameraDistance = Mathf.SmoothDamp(follow.CameraDistance, targetDistance, ref distanceVelocity, cameraDistanceSmoothTime);
    }
}
