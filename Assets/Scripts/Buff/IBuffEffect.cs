using UnityEngine;

public interface IBuffEffect
{
    void OnApply(BuffRuntime buff);

    void OnRemove(BuffRuntime buff);
}
