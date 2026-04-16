using UnityEngine;

public class Interact : MonoBehaviour
{
    private InteractEvent interact = new InteractEvent();
    private PlayerAction player;

    public InteractEvent GetInteractEvent
    {
        get
        {
            if (interact == null) interact = new InteractEvent();
            return interact;
        }
    }

    public PlayerAction GetPlayerAction
    {
        get
        {
            return player;
        }
    }

    public void CallInteract(PlayerAction interactingPlayer)
    {
        player = interactingPlayer;
        interact.CallInteractEvent();
    }
}

public class InteractEvent
{
    public delegate void InteractHandler();
    public event InteractHandler HasInteracted;

    public void CallInteractEvent() => HasInteracted?.Invoke();
}

