using UnityEngine;

public class CharacterStatsController : MonoBehaviour
{
    public CharacterStats Stats { get; private set; }

    private void Awake()
    {
        Stats = new CharacterStats();
    }
}
