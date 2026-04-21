using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "RPG/Dialogue")]
public class DialogueData : ScriptableObject
{
    public string speaker;
    [TextArea]
    public List<string> lines;
}
