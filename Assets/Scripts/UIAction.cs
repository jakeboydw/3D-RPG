using UnityEngine;
using UnityEngine.InputSystem;

public class UIAction : MonoBehaviour
{
    public void OnConfirm()
    {
        if (DialogueManager.Instance && DialogueManager.Instance.IsTalking())
        {
            DialogueManager.Instance.ShowNext();
            return;
        }

        if (InventoryController.Instance && InventoryController.Instance.IsOpen())
        {
            InventoryController.Instance.Confirm();
            return;
        }
    }

    public void OnNavigate(InputValue value)
    {
        Vector2 dir = value.Get<Vector2>();
        InventoryController.Instance.Navigate(dir);
    }

    public void OnCancel()
    {
        if (InventoryController.Instance && InventoryController.Instance.IsOpen())
        {
            InventoryController.Instance.Close();
            return;
        }
    }
}
