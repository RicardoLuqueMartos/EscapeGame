using UnityEngine;

public class SimonLever : MonoBehaviour, iInteractable
{
    [Header("Simon Game")]
    [Tooltip("Nom de la couleur de ce levier (Red, Blue, Green, Yellow)")]
    public string colorName;

    [Tooltip("Référence au SimonGameManager dans la scène")]
    public SimonGameManager gameManager;

    [Header("Animation")]
    [Tooltip("Animator attaché à ce levier")]
    public Animator animator;

    [Tooltip("Nom du Trigger d’animation (dans Animator)")]
    public string animationTrigger = "ButtonPress";

    [Header("Interaction Info")]
    [Tooltip("Texte affiché quand on vise le levier (UI)")]
    public string infoText = "Appuyez sur E pour activer le levier";

    [Header("Audio")]
    [Tooltip("Source audio pour le son du levier")]
    public AudioSource audioSource;

    [Tooltip("Clip audio à jouer à l’activation")]
    public AudioClip leverSound;

    private bool isBeingControlled = false;

    public void IsInteractable(RaycastHit hit)
    {
        if (!isBeingControlled)
        {
            // UIManager.instance.InteractedInfoText(infoText);
            // UIManager.instance.ShowInfoText();
        }
    }

    public void Interact(PlayerInteract player)
    {
        // 🔒 Bloque si le jeu n’est pas prêt pour l’interaction
        if (gameManager != null && !gameManager.canInteract)
            return;

        isBeingControlled = true;
        player.isInteracting = true;
        player.interactingTarget = this;

        if (audioSource != null && leverSound != null)
        {
            audioSource.PlayOneShot(leverSound);
        }

        if (gameManager != null && !string.IsNullOrEmpty(colorName))
        {
            gameManager.HandleInput(colorName);
        }

        if (animator != null && !string.IsNullOrEmpty(animationTrigger))
        {
            animator.SetTrigger(animationTrigger);
        }

        LeaveInteract();
    }

    public void LeaveInteract()
    {
        isBeingControlled = false;
        // UIManager.instance.HideInfoText();
    }

    public bool isControlled()
    {
        return isBeingControlled;
    }
}
