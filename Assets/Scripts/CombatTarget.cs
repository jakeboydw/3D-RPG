using UnityEngine;

public class CombatTarget : MonoBehaviour
{
    public int maxHealth = 50;
    public int baseAttackForce = 5;

    private int health;
    private int attackForce;

    private bool isDead = false;

    private Animator anim;

    private void Start()
    {
        health = maxHealth;
        attackForce = baseAttackForce;

        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        anim.SetTrigger("Hit");

        if (health <= 0)
        {
            isDead = true;
            anim.SetBool("Die", isDead);
        }
    }
}
