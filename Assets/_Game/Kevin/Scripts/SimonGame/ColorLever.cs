using UnityEngine;

public class ColorLever : MonoBehaviour
{
    public string colorName; // "Rouge", "Jaune", etc.
    public SimonGameManager manager;

    public void Activate()
    {
        manager.HandleInput(colorName);
    }

    private void OnMouseDown() // Simple clic souris
    {
        Activate();
    }
}
