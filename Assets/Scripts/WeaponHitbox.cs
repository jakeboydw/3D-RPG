using System.Collections.Generic;
using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    private Combat owner;
    private Collider hitbox;
    private readonly HashSet<Health> hitTargets = new HashSet<Health>();

    private void Awake()
    {
        hitbox = GetComponent<Collider>();
    }

    public void Initialize(Combat owner)
    {
        this.owner = owner;
    }

    public void EnableHitbox()
    {
        hitbox.enabled = true;
        hitTargets.Clear();
    }

    public void DisableHitbox()
    {
        hitbox.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health == null) return;
        if (hitTargets.Contains(health)) return;

        hitTargets.Add(health);
        owner.DealWithDamage(health);
    }
}
