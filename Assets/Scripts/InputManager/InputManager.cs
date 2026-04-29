using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;

    public static InputManager Instance => instance;

    public InputActionAsset actions;

    private InputActionMap playerMap;
    private InputActionMap uiMap;
    private InputActionMap globalMap;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        playerMap = actions.FindActionMap("Player");
        uiMap = actions.FindActionMap("UI");
        globalMap = actions.FindActionMap("Global");

        EnablePlayerInput();
        globalMap.Enable();
    }

    private void OnEnable()
    {
        globalMap.FindAction("ToggleInventory").started += OnToggleInventory;
    }

    private void OnDisable()
    {
        globalMap.FindAction("ToggleInventory").started -= OnToggleInventory;
    }

    public void EnablePlayerInput()
    {
        playerMap.Enable();
        uiMap.Disable();
    }

    public void EnableUIInput()
    {
        uiMap.Enable();
        playerMap.Disable();
    }

    public void OnToggleInventory(InputAction.CallbackContext ctx)
    {
        if (InventoryController.Instance)
        {
            InventoryController.Instance.Toggle();
        }
    }
}
