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

    private void OnEnable()
    {
        model.onChanged += RefreshView;
    }

    private void OnDisable()
    {
        model.onChanged -= RefreshView;
    }

    // === 外部调用接口 ===

    public void AddItem(string itemID, int amount = 1)
    {
        model.Add(itemID, amount);

        EventCenter.Publish(new GartherItemEvent
        {
            itemID = itemID,
            amount = amount
        });
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
        currentIndex = Mathf.Clamp(index, 0, view.slots.Count - 1);
        view.SetSelected(currentIndex);

        UpdateDescription();
    }

    public void Navigate(Vector2 dir)
    {
        if (!isOpen) return;

        int index = currentIndex;
        if (dir.x > 0) index++;
        if (dir.x < 0) index--;
        if (dir.y > 0) index -= column;
        if (dir.y < 0) index += column;

        Select(index);
    }

    // === 交互 ===

    public void Confirm()
    {
        if (!isOpen) return;

        //如果已经打开操作面板，直接执行操作
        if (isActionPanelOpen)
        {
            //执行操作
        }

        //否则打开面板
        ItemData item = GetCurrentItem();
        if (item == null) return;
    }

    public void UseItem(ItemData item)
    {

    }
}
