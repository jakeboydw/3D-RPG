using System.Collections.Generic;
using UnityEngine;

public class BuffSystem : MonoBehaviour
{
    private static BuffSystem instance;

    public static BuffSystem Instance => instance;

    private List<BuffRuntime> activeBuffs = new();

    private void Awake()
    {
        //Buff系统单例
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void ApplyBuff(BuffConfig config, GameObject target)
    {
        BuffRuntime buff = new BuffRuntime(config, target);

        foreach (var effect in buff.effects)
        {
            effect.OnApply(buff);
        }

        activeBuffs.Add(buff);
    }

    private void Update()
    {
        float dt = Time.deltaTime;

        for (int i = activeBuffs.Count - 1; i >= 0; i--)
        {
            var buff = activeBuffs[i];

            buff.Tick(dt);

            if (buff.IsFinished)
            {
                foreach (var effect in buff.effects)
                {
                    effect.OnRemove(buff); //清除Buff效果
                }

                activeBuffs.Remove(buff); //移除Buff
            }
        }
    }
}
