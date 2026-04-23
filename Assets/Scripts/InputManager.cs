using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;

    public static InputManager Instance => instance;

    public InputActionAsset actions;

    private InputActionMap playerMap;
    private InputActionMap uiMap;

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

        EnablePlayerInput();
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
}
