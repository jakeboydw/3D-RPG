using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    private static ItemDatabase instance;

    public static ItemDatabase Instance => instance;

    public List<ItemConfig> items;
    
    private Dictionary<string, ItemConfig> dict;

    private void Awake()
    {
        //物品数据库单例
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        dict = new Dictionary<string, ItemConfig>();
        foreach (ItemConfig item in items)
        {
            dict[item.itemID] = item;
        }
    }

    public ItemConfig Get(string id)
    {
        if (dict.TryGetValue(id, out var item)) return item;
        return null;
    }
}
