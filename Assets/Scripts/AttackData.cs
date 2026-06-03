using UnityEngine;

[CreateAssetMenu(fileName = "NewAttack", menuName = "RPG/Attack")]
public class AttackData : ScriptableObject
{
    public float damage;
    public AnimationClip attackAnimation;
    public AttackData nextAttack;
}
