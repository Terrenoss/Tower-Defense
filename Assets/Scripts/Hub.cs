using UnityEngine;

public class Hub : Interactable
{
    [SerializeField] private int Health = 20;

    // [MODIF] + int damage parameter (and void return type, missing from the diagram):
    // without this, the Hub cannot know how much damage the enemy deals (Enemy.damage is private).
    public void OnTakeDamage(int damage)
    {
        if (Health <= 0)
            return;

        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;
            Debug.Log("GAME OVER");
            Time.timeScale = 0f; // [ADDITION] no end-of-game handling in the diagram
        }
    }

    // [ADD][DEBUG] Minimal display to allow gameplay.
    private void OnGUI()
    {
        GUI.matrix = Matrix4x4.Scale(Vector3.one * Screen.height / 360f);
        GUI.Label(new Rect(10, 30, 300, 20), $"PV du Hub : {Health}" + (Health <= 0 ? "  -  GAME OVER" : ""));
    }
}
