using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

// [ADDED] Class missing from the diagram: nothing was calling Interactable.Interact().
// On left-click, calls Interact() on the Interactable under the mouse.
public class InteractionHandler : MonoBehaviour
{
    private void Update()
    {
        if (Mouse.current == null || !Mouse.current.leftButton.wasPressedThisFrame)
            return;
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        foreach (Collider2D hit in Physics2D.OverlapPointAll(worldPosition))
        {
            if (hit.TryGetComponent(out Interactable interactable))
            {
                interactable.Interact();
                return;
            }
        }
    }
}
