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

    public List<AttackData> attacks = new List<AttackData>();
    public AttackData firstAttack;
    public AttackData CurrentAttack { get; private set; }

    private float attackForce;

    private bool comboQueued = false;
    private bool comboWindowOpen = false;

    private Animator anim;
    private Character character;
    private PlayerMovement movementController;
    private PlayerFSM fsm;
    private WeaponHitbox hitbox;

    private void Start()
    {
        anim = GetComponent<Animator>();
        character = GetComponent<Character>();
        movementController = GetComponent<PlayerMovement>();
        fsm = GetComponent<PlayerFSM>();
        hitbox = GetComponentInChildren<WeaponHitbox>();

        attackForce = character.Stats.GetStat(StatType.Attack).Value;

        hitbox.Initialize(this);
    }

    private void Update()
    {
        attackForce = character.Stats.GetStat(StatType.Attack).Value;
    }

    public void OnAttack()
    {
        if (fsm.CurrentStateType != PlayerStateType.Attack)
        {
            CurrentAttack = firstAttack;
            fsm.ChangeState(PlayerStateType.Attack);
            return;
        }

        if (comboWindowOpen)
        {
            comboQueued = true;
        }
    }

    public void EnableHitbox()
    {
        hitbox.EnableHitbox();
    }

    public void DisableHitbox()
    {
        hitbox.DisableHitbox();
    }

    public void DealWithDamage(Health target)
    {
        DamageInfo damageInfo = new DamageInfo
        {
            attacker = gameObject,
            target = target.gameObject,
            damage = attackForce
        };

        target.TakeDamage(damageInfo.damage);

        if (target.IsDead())
        {
            target.GetComponent<Character>()?.OnDie();
        }

        target.GetComponent<Character>()?.OnHit();
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

    public void OpenComboWindow()
    {
        comboWindowOpen = true;
    }

    public void ProcessCombo()
    {
        comboWindowOpen = false;

        if (comboQueued && CurrentAttack.nextAttack != null)
        {
            comboQueued = false;
            CurrentAttack = CurrentAttack.nextAttack;
            fsm.ChangeState(PlayerStateType.Attack);
            return;
        }

        EndCombo();
    }

    public void EndCombo()
    {
        comboQueued = false;
        comboWindowOpen = false;

        CurrentAttack = null;

        fsm.ChangeState(PlayerStateType.Locomotion);
    }
}
