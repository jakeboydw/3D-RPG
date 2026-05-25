using UnityEngine;

public class Health : MonoBehaviour
{
    private Character character;

    public float HP;
    private bool isDead = false;

    private void Start()
    {
        character = GetComponent<Character>();

        HP = character.Stats.GetStat(StatType.MaxHP).Value;
    }

    public void Heal(float heal)
    {
        if (isDead) return;

        HP = Mathf.Clamp(HP + heal, 0, character.Stats.GetStat(StatType.MaxHP).Value);
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        HP = Mathf.Clamp(HP - damage, 0, character.Stats.GetStat(StatType.MaxHP).Value);

        if (HP == 0) isDead = true;
    }

    public bool IsDead()
    {
        return isDead;
    }
}
