using System.Collections.Generic;
using UnityEngine;

public class BuffRuntime
{
    public BuffConfig config;

    public float remainingTime;

    public GameObject owner;

    public List<IBuffEffect> effects = new List<IBuffEffect>();

    public bool IsFinished => remainingTime <= 0;

    public BuffRuntime(BuffConfig config, GameObject owner)
    {
        this.config = config;
        this.owner = owner;

        remainingTime = config.duration;

        foreach (var effect in this.config.effects)
        {
            effects.Add(BuffEffectFactory.Create(effect));
        }
    }

    public void Tick(float deltaTime)
    {
        remainingTime -= deltaTime;
    }
}
