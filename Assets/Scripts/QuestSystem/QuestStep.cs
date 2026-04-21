using UnityEngine;

[System.Serializable]
public abstract class QuestStep
{
    public string stepDescription;

    public abstract void OnStart(QuestStatus questStatus);
    public abstract void OnFinish();

    public abstract string GetProgressText();
}