using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private static InventoryController instance;

    public static InventoryController Instance => instance;

    public InventoryView view;
    public int column = 5;

    private InventoryModel model;
    private int currentIndex = 0;
    private bool isOpen = false;
    private bool isActionPanelOpen = false;

    private void Awake()
    {
        //库存控制器单例
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        model = new InventoryModel();
    }

    private void Start()
    {
        Close();
    }

    private void OnEnable()
    {
        model.onChanged += RefreshView;
    }

    private void OnDisable()
    {
        model.onChanged -= RefreshView;
    }

    // === 外部调用接口 ===
    public int Count(string itemID)
    {
        int total = 0;
        foreach (var item in model.Items)
        {
            if (item.itemID == itemID)
            {
                total += item.amount;
            }
        }
        return total;
    }

    public void AddItem(string itemID, int amount = 1)
    {
        model.Add(itemID, amount);

        EventCenter.Publish(new CollectItemEvent
        {
            itemID = itemID,
            amount = amount
        });
    }

    public void RemoveItem(string itemID, int amount = 1)
    {
        model.Remove(itemID, amount);
    }

    public bool IsOpen()
    {
        return isOpen;
    }

    public void Toggle()
    {
        if (isOpen) Close();
        else Open();
    }

    public void Cancel()
    {
        if (isActionPanelOpen)
        {
            ActionPanel.Instance.Close();
            return;
        }

        Close();
    }

    public void Open()
    {
        view.Show(true);
        isOpen = true;

        RefreshView();
        Select(0);

        InputManager.Instance.EnableUIInput();
    }

    public void Close()
    {
        view.Show(false);
        isOpen = false;

        InputManager.Instance.EnablePlayerInput();
    }

    // === UI ===

    private void RefreshView()
    {
        view.Refresh(model.Items);
    }

    private ItemData GetCurrentItem()
    {
        if (currentIndex >= model.Items.Count)
        {
            return null;
        }

        return model.Items[currentIndex];
    }

    private void UpdateDescription()
    {
        ItemData item = GetCurrentItem();

        view.SetDescription(item);
    }

    // === 选择 ===

    public void Select(int index)
    {
        currentIndex = view.ClampIndex(index);
        view.SetSelected(currentIndex);

        UpdateDescription();
    }

    public void Navigate(Vector2 dir)
    {
        if (!isOpen) return;

        if (isActionPanelOpen)
        {
            ActionPanel.Instance.Navigate(dir);
            return;
        }

        int index = currentIndex;

        if (dir.x > 0) index++;
        if (dir.x < 0) index--;
        if (dir.y > 0 && index >= column) index -= column;
        if (dir.y < 0 && index < view.slotCount - column) index += column;

        Select(index);
    }

    // === 交互 ===

    public void SetActionPanelState(bool state)
    {
        isActionPanelOpen = state;
    }

    public void Confirm()
    {
        if (!isOpen) return;

        //如果已经打开操作面板，直接执行操作
        if (isActionPanelOpen)
        {
            ActionPanel.Instance.Confirm();
            return;
        }

        //否则打开面板
        ItemData item = GetCurrentItem();
        if (item == null) return;

        ActionPanel.Instance.Open(item, view.GetSlotTransform(currentIndex));
    }

    public void UseItem(ItemData item)
    {
        //can add buff here
    }
}
