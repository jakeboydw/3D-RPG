using System;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    public Image highlight;

    private Action action;

    public void SetAction(Action a)
    {
        action = a;
    }

    public void Execute()
    {
        action?.Invoke();
    }

    public void SetSelected(bool selected)
    {
        highlight.enabled = selected;
    }
}
