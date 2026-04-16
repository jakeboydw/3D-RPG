using UnityEngine;

public class RequestGiver : MonoBehaviour
{
    public Interact interact;

    private void OnEnable()
    {
        if (interact)
        {
            interact.GetInteractEvent.HasInteracted += GiveRequest;
        }
    }

    private void OnDisable()
    {
        if (interact)
        {
            interact.GetInteractEvent.HasInteracted -= GiveRequest;
        }
    }

    private void GiveRequest()
    {
        //give player a request
        Debug.Log("a request");
    }
}
