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

        //只有距离玩家最近的物体才可以互动
        if (closestObject)
        {
            var interactable = closestObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }
}
