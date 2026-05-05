using System.Collections.Generic;
using UnityEngine;

public class DialogueDatabase : MonoBehaviour
{
    private static DialogueDatabase instance;

    public static DialogueDatabase Instance => instance;

    public List<TextAsset> dialogueJsonFiles;

    private Dictionary<string, DialogueData> dict = new();

    private void Awake()
    {
        //对话数据库单例
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

        foreach (var file in dialogueJsonFiles)
        {
            DialogueData data = JsonUtility.FromJson<DialogueData>(file.text);
            dict[data.dialogueID] = data;
        }
    }

    public DialogueData Get(string id)
    {
        if (dict.TryGetValue(id, out var data))
        {
            return data;
        }

        return null;
    }
}
