using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;

    public static DialogueManager Instance => instance;

    public GameObject dialogueBox;
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI contentText;

    public float typingDuration = 0.8f;

    private Queue<string> sentences = new Queue<string>();
    private Action onDialogueEnd;
    private bool isTalking = false;

    private Tween typingTween;
    private bool isTyping = false;

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
        //如果正在打字，再次输入则直接显示完整句子
        if (isTyping)
        {
            typingTween.Complete();
            return;
        }

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string line = sentences.Dequeue();
        contentText.text = "";

        typingTween?.Kill();

        isTyping = true;
        
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
