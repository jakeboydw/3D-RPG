using System;
using UnityEngine;

public class EventCenter : MonoBehaviour
{
    public event Action Interact;
    public event Action<string> GartherItem;
    public event Action TalkToNPC;
    public event Action KillEnemy;

    public void CallInteract() => Interact?.Invoke();
    public void CallGartherItem(string itemID) => GartherItem?.Invoke(itemID);
    public void CallTalkToNPC() => TalkToNPC?.Invoke();
    public void CallKillEnemy() => KillEnemy?.Invoke();
}
