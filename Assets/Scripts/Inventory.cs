using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static Inventory instance;
    public static Inventory Instance => instance;

    public EventCenter eventCenter;

    private List<Item> items = new List<Item>();

    private void Awake()
    {
        //库存单例
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void Add(Item item)
    {
        items.Add(item);

        //只有当物品入库，才触发信号
        eventCenter.CallGartherItem(item.itemID);
    }

    public void Remove(Item item)
    {
        items.Remove(item);
    }
}
