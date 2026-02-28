using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public SpecificClickManager clickManager;

    private void Update()
    {
        if (!Mouse.current.leftButton.wasPressedThisFrame) return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider)
        {
            // Only the clicked object handles its toggle
            var clickable = hit.collider.GetComponent<ClickableObject>();
            if (!clickable) return;
            clickable.Toggle();
            clickManager.ObjectClicked();
        }
    }
}