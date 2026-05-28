using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ActionPanel : MonoBehaviour
{
    private static ActionPanel instance;

    public static ActionPanel Instance => instance;

    public RectTransform panel;
    public List<ActionButton> buttons;
    public Vector3 panelOffset = new Vector3(20, 0, 0);

    private int currentIndex = 0; //操作按钮的坐标
    private ItemData currentItem;
    private List<ActionButton> activeButtons = new List<ActionButton>();

    private void Awake()
    {
        //物品操作面板单例
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        panel.localScale = Vector3.zero;
        panel.gameObject.SetActive(false);
    }

    public void Open(ItemData item, Transform slot)
    {
        panel.DOKill();

        currentItem = item;
        InventoryController.Instance.SetActionPanelState(true);

        panel.position = slot.position + panelOffset;

        SetupButtons();
        Select(0);

        panel.gameObject.SetActive(true);
        panel.localScale = Vector3.zero;
        panel.DOScale(Vector3.one, 0.15f).SetEase(Ease.OutBack);
    }

    private void SetupButtons()
    {
        activeButtons.Clear();

        ItemConfig config = ItemDatabase.Instance.Get(currentItem.itemID);

        // === Use ===
        if (config.canUse)
        {
            buttons[0].gameObject.SetActive(true);
            buttons[0].SetAction(() =>
            {
                InventoryController.Instance.UseItem(currentItem);
            });
            activeButtons.Add(buttons[0]);
        }
        else
        {
            buttons[0].gameObject.SetActive(false);
        }

        // === Drop ===
        if (config.canDrop)
        {
            buttons[1].gameObject.SetActive(true);
            buttons[1].SetAction(() =>
            {
                //drop item
            });
            activeButtons.Add(buttons[1]);
        }
        else
        {
            buttons[1].gameObject.SetActive(false);
        }

        // === Cancel ===
        buttons[2].gameObject.SetActive(true);
        buttons[2].SetAction(() =>
        {
            Close();
        });
        activeButtons.Add(buttons[2]);
    }

    public void Close()
    {
        panel.DOKill();

        panel.DOScale(Vector3.zero, 0.12f).SetEase(Ease.InBack).OnComplete(() =>
        {
            panel.gameObject.SetActive(false);
        });

        InventoryController.Instance.SetActionPanelState(false);
    }

    // === 选择 ===
    public void Navigate(Vector2 dir)
    {
        if (activeButtons.Count == 0) return;

        int index = currentIndex;
        if (dir.y > 0) index--;
        if (dir.y < 0) index++;

        Select(index);
    }

    private void Select(int index)
    {
        if (activeButtons.Count == 0) return;

        currentIndex = Mathf.Clamp(index, 0, activeButtons.Count - 1);

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].SetSelected(false);
        }

        activeButtons[currentIndex].SetSelected(true);
    }

    public void Confirm()
    {
        if (activeButtons.Count == 0) return;
        activeButtons[currentIndex].Execute();
    }
}
