using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "RPG/Item")]
public class ItemConfig : ScriptableObject
{
    public string itemID;
    public string itemName;
    public Sprite icon;
    [TextArea]
    public string description;
}
