using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterStats Stats { get; private set; }

    public float baseMaxHP;
    public float baseAttack;
    public float baseMoveSpeed;

    private Animator anim;

    private void Awake()
    {
        Stats = new CharacterStats();

        //初始化角色数据
        Stats.GetStat(StatType.MaxHP).BaseValue = baseMaxHP;
        Stats.GetStat(StatType.Attack).BaseValue = baseAttack;
        Stats.GetStat(StatType.MoveSpeed).BaseValue = baseMoveSpeed;

        anim = GetComponent<Animator>();
    }

    public void OnHit()
    {
        anim.SetTrigger("Hit");
    }

    public void OnDie()
    {
        anim.SetBool("Die", true);

        GetComponent<Collider>().enabled = false;
    }
}
