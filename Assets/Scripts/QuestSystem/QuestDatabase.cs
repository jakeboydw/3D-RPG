using System.Collections.Generic;
using UnityEngine;

public class QuestDatabase : MonoBehaviour
{
    private static QuestDatabase instance;

    public static QuestDatabase Instance => instance;

    public List<TextAsset> questJsonFiles;

    private Dictionary<string, QuestData> dict = new();

    private void Awake()
    {
        //任务数据库单例
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
        dict.Clear();

        foreach (var file in questJsonFiles)
        {
            QuestData data = JsonUtility.FromJson<QuestData>(file.text);
            dict[data.questID] = data;
        }
    }

    public QuestData Get(string id)
    {
        if (dict.TryGetValue(id, out var data))
        {
            return data;
        }

        return null;
    }
}
