using TMPro;
using UnityEngine;

public class PropertyUI : MonoBehaviour
{
    public TextMeshProUGUI attack;
    public TextMeshProUGUI speed;

    public Character player;

    private void Update()
    {
        attack.text = "攻击力：" + player.Stats.GetStat(StatType.Attack).Value.ToString();
        speed.text = "移速：" + player.Stats.GetStat(StatType.MoveSpeed).Value.ToString();
    }
}
