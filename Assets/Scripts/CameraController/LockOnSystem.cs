using System.Collections.Generic;
using UnityEngine;

public class LockOnSystem : MonoBehaviour
{
    public Transform cameraRoot;

    public float lockRadius = 10f;
    public float lockAngle = 90f;

    public LayerMask enemyLayer;
    public LayerMask obstacleLayer;

    public Transform CurrentTarget { get; private set; }

    public bool IsLocked => CurrentTarget != null;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        ValidateCurrentTarget();
    }

    public void OnLockOn()
    {
        if (IsLocked)
        {
            ClearLock();
        }
        else
        {
            FindBestTarget();
        }
    }

    public void ClearLock()
    {
        CurrentTarget = null;
    }

    private void ValidateCurrentTarget()
    {
        if (CurrentTarget == null)
        {
            return;
        }

        Health health = CurrentTarget.GetComponent<Health>();
        if (health != null && health.IsDead())
        {
            ClearLock();
            return;
        }

        float distance = Vector3.Distance(transform.position, CurrentTarget.position);
        if (distance > lockRadius)
        {
            ClearLock();
        }
    }

    private void FindBestTarget()
    {
        List<Transform> candidates = DetectTargets();

        if (candidates.Count == 0)
        {
            ClearLock();
            return;
        }

        float bestScore = float.MaxValue;
        Transform bestTarget = null;

        //更靠近屏幕中心的目标会被选中
        foreach (var target in candidates)
        {
            Vector3 viewportPos = mainCamera.WorldToViewportPoint(target.position);
            float score = Vector2.Distance(new Vector2(viewportPos.x, viewportPos.y), new Vector2(0.5f, 0.5f));
            if (score < bestScore)
            {
                bestScore = score;
                bestTarget = target;
            }
        }

        CurrentTarget = bestTarget;
    }

    private List<Transform> DetectTargets()
    {
        List<Transform> result = new();

        Collider[] hits = Physics.OverlapSphere(transform.position, lockRadius, enemyLayer);

        Vector3 forward = cameraRoot.forward;
        forward.y = 0f;
        forward.Normalize();

        float cos = Mathf.Cos(lockAngle * 0.5f * Mathf.Deg2Rad);

        foreach (var hit in hits)
        {
            Transform target = hit.transform;

            Vector3 targetPos = target.position;
            targetPos.y = transform.position.y;
            Vector3 dir = (targetPos - transform.position).normalized;
            if (Vector3.Dot(dir, forward) < cos)
            {
                continue;
            }

            if (IsBlocked(target))
            {
                continue;
            }
            
            result.Add(target);
        }

        return result;
    }

    private bool IsBlocked(Transform target)
    {
        Vector3 origin = mainCamera.transform.position;
        Vector3 targetPos = target.position;
        Vector3 dir = targetPos - origin;
        float distance = dir.magnitude;

        return Physics.Raycast(origin, dir.normalized, distance, obstacleLayer);
    }
}
