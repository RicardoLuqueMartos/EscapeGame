using UnityEngine;

public class PlayerBreath : MonoBehaviour
{
    [SerializeField] int maxBreathPoints = 100;
    public int currentBreathPoints = 100;
    public bool canBreath = true;

    [SerializeField] int amountToLoose = 1;
    [SerializeField] float frequencyToLoose = 1f;

    void Start()
    {
        ResetHitPoints();
    }

    private void OnEnable()
    {
        InvokeRepeating("Drowning", frequencyToLoose, frequencyToLoose);
    }

    void ResetHitPoints()
    {
        SetCurrentToMaxHitPoints();
    }

    void SetCurrentToMaxHitPoints()
    {
        currentBreathPoints = maxBreathPoints;
    }

    public void CantBreath()
    {
        canBreath = false;
    }

    private void Drowning()
    {
        if (canBreath == false)
        {
            if (currentBreathPoints > 0) RemoveBreathPoints();            
            else RemoveHealth();
        }
    }

    private void RemoveBreathPoints()
    {
        currentBreathPoints = currentBreathPoints - amountToLoose;
        ShowDrowningImage();
    }

    void ShowDrowningImage()
    {
        ScreenDamage.instance.blueBlurImage.gameObject.SetActive(true);
        Invoke("HideDrowningImage", .05f);
    }

    void HideDrowningImage()
    {
        ScreenDamage.instance.blueBlurImage.gameObject.SetActive(false);
    }


    private void RemoveHealth()
    {
        PlayerHealth.instance.RemovehitPoints(amountToLoose);
    }
}
