using System.Collections.Generic;
using UnityEngine;

public struct DamageInfo
{
    public float damage;
    public GameObject target;
    public GameObject attacker;
}

public class Combat : MonoBehaviour
{
    public float radius = 3f;
    public float angle = 90f;
    public LayerMask targetLayer;

    public AttackData CurrentAttack { get; private set; }

    private float attackForce;

    private int comboIndex = 0;
    private bool comboQueued = false;
    private bool canQueueCombo = false;

    private Animator anim;
    private Character character;
    private PlayerMovement movementController;

    private void Start()
    {
        anim = GetComponent<Animator>();
        character = GetComponent<Character>();
        movementController = GetComponent<PlayerMovement>();
        attackForce = character.Stats.GetStat(StatType.Attack).Value;
    }

    private void Update()
    {
        attackForce = character.Stats.GetStat(StatType.Attack).Value;

        UpdateComboWindow();
        ProcessComboQueue();
        ResetCombo();
    }

    public void OnAttack()
    {
        //第一段攻击
        if (!character.IsAttacking)
        {
            StartCombo();
            return;
        }

        if (canQueueCombo)
        {
            comboQueued = true;
        }
    }

    private void UpdateComboWindow()
    {
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);

        if (!character.IsAttacking)
        {
            canQueueCombo = false;
            return;
        }

        float progress = state.normalizedTime % 1f;
        canQueueCombo = progress >= 0.3f && progress <= 0.9f;
    }

    private void ProcessComboQueue()
    {
        if (!comboQueued) return;

        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
        float progress = state.normalizedTime % 1f;

        if (progress < 0.9f) return;

        comboIndex++;
        comboIndex = Mathf.Clamp(comboIndex, 1, 3);
        anim.SetInteger("ComboIndex", comboIndex);
    }

    private void ResetCombo()
    {
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
        float progress = state.normalizedTime % 1f;

        if (progress > 0.98f && !comboQueued)
        {
            EndCombo();
        }
    }

    private void StartCombo()
    {
        comboIndex = 1;

        character.StartAttack();

        anim.SetInteger("ComboIndex", comboIndex);
        anim.SetTrigger("Attack");
    }

    public void AttackHit()
    {
        List<Collider> targets = DetectTargets();
        movementController.RotateToAttackTarget(targets);

        foreach (Collider target in targets)
        {
            DamageInfo damageInfo = new DamageInfo
            {
                attacker = gameObject,
                target = target.gameObject,
                damage = attackForce
            };

            Health health = target.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damageInfo.damage);

                if (health.IsDead())
                {
                    target.GetComponent<Character>()?.OnDie();
                }
            }

            target.GetComponent<Character>()?.OnHit();
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

    public void EndCombo()
    {
        comboQueued = false;
        canQueueCombo = false;

        comboIndex = 0;
        anim.SetInteger("ComboIndex", comboIndex);

        character.EndAttack();
    }
}
