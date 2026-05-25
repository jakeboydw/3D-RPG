using UnityEngine;

public class Detector : MonoBehaviour
{
    public float detectRadius = 5f;
    public LayerMask playerLayer;
    public Transform target = null;

    private Collider[] results = new Collider[10];

    private void DetectPlayer()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, detectRadius, results, playerLayer);
        if (count > 0 )
        {
            target = results[0].transform;
        }
        else
        {
            target = null;
        }
    }

    public bool HasTarget()
    {
        DetectPlayer();
        return target != null;
    }
}
