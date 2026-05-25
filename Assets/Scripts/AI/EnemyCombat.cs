using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public float damage;

    public void Attack(GameObject target)
    {
        var health = target.GetComponent<Health>();
        if (health != null )
        {
            health.TakeDamage(damage);
        }
    }
}
