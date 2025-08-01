using UnityEngine;

public class SimonGameTrigger : MonoBehaviour
{
    [Header("Simon Game")]
    public SimonGameManager simonGameManager;
    private bool hasStarted = false;

    [Header("Murs")]
    public Animator leftWallAnimator;
    public Animator rightWallAnimator;
    public string closeTriggerName = "Close";

    private void OnTriggerEnter(Collider other)
    {
        if (hasStarted) return;

        if (other.CompareTag("Player"))
        {
            hasStarted = true;

            // ✅ Lance les animations de fermeture
            if (leftWallAnimator != null) leftWallAnimator.SetTrigger(closeTriggerName);
            if (rightWallAnimator != null) rightWallAnimator.SetTrigger(closeTriggerName);

            // ✅ Lance l’énigme
            if (simonGameManager != null) simonGameManager.StartSimonGame();
        }
    }
}
