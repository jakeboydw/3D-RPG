using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float distance = 15f;
    public LayerMask interactableLayer;

    private float minDistance = float.MaxValue;
    private Collider closestObject;

    public void OnInteract()
    {
        //每次交互前初始化参数
        minDistance = float.MaxValue;
        closestObject = null;

        Collider[] colliders = Physics.OverlapSphere(transform.position, distance, interactableLayer);

        foreach (Collider collider in colliders)
        {
            if (Vector3.Distance(transform.position, collider.transform.position) < minDistance)
            {
                closestObject = collider;
                minDistance = Vector3.Distance(transform.position, collider.transform.position);
            }
        }

        //只有距离玩家最近且可以互动的物体才会调用事件
        if (closestObject)
        {
            Interact interact = closestObject.GetComponent<Interact>();
            if (interact)
            {
                interact.CallInteract(this);
            }
        }
    }
}
