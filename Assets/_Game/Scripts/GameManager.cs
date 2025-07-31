using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameSettingsData gameSettingsData;

    private void Awake()
    {
        CreateInstance();
    }

    void CreateInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
