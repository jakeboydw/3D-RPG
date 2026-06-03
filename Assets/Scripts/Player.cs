using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator Animator { get; private set; }
    public Combat Combat { get; private set; }
    public PlayerMovement Movement { get; private set; }
    public Health Health { get; private set; }
    public Character Character { get; private set; }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        Combat = GetComponent<Combat>();
        Movement = GetComponent<PlayerMovement>();
        Health = GetComponent<Health>();
        Character = GetComponent<Character>();
    }
}
