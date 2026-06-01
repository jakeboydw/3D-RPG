using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;

    public static DialogueManager Instance => instance;

    public GameObject dialogueBox;
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI contentText;

    private Queue<string> sentences = new Queue<string>();
    private Action onDialogueEnd;
    private bool isTalking = false;

    private void Awake()
    {
        //对话管理器单例
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        dialogueBox.SetActive(false);
    }

    public void StartDialogue(string dialogueID, Action onEnd = null)
    {
        var data = DialogueDatabase.Instance.Get(dialogueID);

        isTalking = true;
        dialogueBox.SetActive(true);

        InputManager.Instance.EnableUIInput();

        speakerText.text = data.speaker;

        sentences.Clear();
        foreach (string line in data.lines)
        {
            sentences.Enqueue(line);
        }

        onDialogueEnd = onEnd;

        ShowNext();
    }

    public void ShowNext()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        contentText.text = sentences.Dequeue();
    }

    private void EndDialogue()
    {
        isTalking = false;
        dialogueBox.SetActive(false);

        InputManager.Instance.EnablePlayerInput();

        onDialogueEnd?.Invoke();
    }

    public bool IsTalking()
    {
        return isTalking;
    }
}
