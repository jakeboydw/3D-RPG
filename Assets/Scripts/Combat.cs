using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public int maxHealth = 100;
    public int baseAttackForce = 20;

    public float radius = 3f;
    public float angle = 90f;
    public LayerMask targetLayer;

    private int health;
    private int attackForce;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        health = maxHealth;
        attackForce = baseAttackForce;
    }

    public void OnAttack()
    {
        anim.SetTrigger("Attack");

        List<Collider> targets = DetectTargets();
        foreach (Collider target in targets)
        {
            CombatTarget combatTarget = target.GetComponent<CombatTarget>();
            if (combatTarget != null)
            {
                combatTarget.TakeDamage(attackForce);
            }
        }
    }

    private List<Collider> DetectTargets()
    {
        List<Collider> result = new List<Collider>();

        Collider[] hits = Physics.OverlapSphere(transform.position, radius, targetLayer);
        foreach (Collider hit in hits)
        {
            Vector3 targetPos = hit.transform.position;
            targetPos.y = transform.position.y;
            Vector3 dir = (targetPos - transform.position).normalized;
            float dot = Vector3.Dot(transform.forward, dir);
            float cos = Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad); //在角色前方的扇形区域进行碰撞体检测
            if (dot >= cos)
            {
                result.Add(hit);
            }
        }

        return result;
    }
}
