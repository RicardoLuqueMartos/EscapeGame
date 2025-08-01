using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 100;
    public int currentHitPoints = 100;


    void Start()
    {
        ResetHitPoints();
    }

    void ResetHitPoints()
    {
        SetCurrentToMaxHitPoints(); 
        ScreenDamage.instance.maxHealth = maxHitPoints;
    }

    void SetCurrentToMaxHitPoints()
    {
        currentHitPoints = maxHitPoints;
    }

    public void RemovehitPoints(int hitPoints)
    {
        currentHitPoints = currentHitPoints - hitPoints;
        ScreenDamage.instance.CurrentHealth -= hitPoints;
        if (currentHitPoints <= 0) {
            KillPlayer();
        }
    }

    public void KillPlayer()
    {

    }
}
