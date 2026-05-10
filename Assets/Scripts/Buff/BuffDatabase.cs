using System.Collections.Generic;
using UnityEngine;

public class BuffDatabase : MonoBehaviour
{
    private static BuffDatabase instance;

    public static BuffDatabase Instance => instance;

    public List<TextAsset> buffJsonFiles;

    private Dictionary<string, BuffConfig> dict = new();

    private void Awake()
    {
        //Buff数据库
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        LoadAll();
    }

    private void LoadAll()
    {
        foreach (var file in buffJsonFiles)
        {
            BuffConfig config = JsonUtility.FromJson<BuffConfig>(file.text);
            dict[config.id] = config;
        }
    }

    public BuffConfig Get(string id)
    {
        if (dict.TryGetValue(id, out var config))
        {
            return config;
        }

        return null;
    }
}
