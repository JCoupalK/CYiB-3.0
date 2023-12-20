using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class KeyboardScrollControl : MonoBehaviour
{
    public ScrollRect scrollRect; // Assign your ScrollRect component here
    private bool isFocused = false;
    private DefaultInputActions inputActions; // Replace with your actual InputActions class

    private void Awake()
    {
        inputActions = new DefaultInputActions();
    }

    private void OnEnable()
    {
        inputActions.UI.Navigate.performed += OnMovePerformed;
        inputActions.UI.Submit.performed += OnEnterPerformed; // Bind the Enter key
        inputActions.UI.Enable();
    }

    private void OnDisable()
    {
        inputActions.UI.Navigate.performed -= OnMovePerformed;
        inputActions.UI.Submit.performed -= OnEnterPerformed; // Unbind the Enter key
        inputActions.UI.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        if (isFocused)
        {
            Vector2 moveInput = context.ReadValue<Vector2>();
            scrollRect.verticalNormalizedPosition += moveInput.y * Time.deltaTime; // Adjust scrolling speed if necessary
        }
    }

    private void OnEnterPerformed(InputAction.CallbackContext context)
    {
        isFocused = !isFocused; // Toggle the focus state
        Debug.Log("isFocused");
    }
}
