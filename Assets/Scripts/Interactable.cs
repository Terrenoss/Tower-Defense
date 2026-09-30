using UnityEngine;

public class Interactable : MonoBehaviour
{
    // [MODIF] virtual: each child must be able to define its own reaction.
    public virtual void Interact()
    {
        Debug.Log($"Interact() sur {name} : aucune action définie par le diagramme.");
    }
}
