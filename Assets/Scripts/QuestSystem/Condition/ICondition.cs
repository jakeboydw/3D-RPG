using System;
using UnityEngine;

public interface ICondition
{
    void Register(Action onComplete);
    void Unregister();
    bool IsMet();
}
