using System.Collections.Generic;
using UnityEngine;

public enum ItemUseType
{
    Self = 0,
    Others = 1
}

[CreateAssetMenu(fileName = "NewItem", menuName = "RPG/Item")]
public class ItemConfig : ScriptableObject
{
    public string itemID;
    public string itemName;
    public Sprite icon;
    [TextArea]
    public string description;

    public bool canUse;
    public bool canDrop;

    public ItemUseType useType;
    public List<string> buffIds = new List<string>();
}
