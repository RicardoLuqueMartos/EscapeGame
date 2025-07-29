using Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Variables
    [SerializeField] GameSettingsData gameSettingsData;

    public static UIManager instance;
    public Image centerCursorImage;
    

    public GameObject GameMenuObject;

    public TMP_Text contextuelInteract;
    public GameObject RefectorQuitPanel;

    [SerializeField] Image tesseractImage;
    [SerializeField] public Image fadingPanel;
    public TMP_Text tesseractAmount;

    [SerializeField] GameObject BaseControlsInfo;
    [SerializeField] GameObject ReflectorsControlsInfo;

    public GameObject WinPanel;
    #endregion Variables

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        HideMenu();
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {            
            if (GameMenuObject.activeInHierarchy) HideMenu();
            else DisplayMenu();
        }
    }

    #region Controls Infos
    public void HideAllControlsInfos()
    {
        BaseControlsInfo.SetActive(false);
        ReflectorsControlsInfo.SetActive(false);
    }

    public void DisplayBaseControlsInfo()
    {
        BaseControlsInfo.SetActive(true);
    }

  
    #endregion Controls Infos

    #region Contextual info
    public void InteractedInfoText(string text)
    {
        contextuelInteract.text = text;
        DisplayInfoText();
    }
    public void DisplayInfoText()
    {
        if (CanDisplayInfoText())
            contextuelInteract.transform.parent.gameObject.SetActive(true);
    }
    public void HideInfoText()
    {
        contextuelInteract.transform.parent.gameObject.SetActive(false);
    }

    bool CanDisplayInfoText()
    {
        bool response = true;
        // here verify

        return response;
    }

    #endregion Contextual info

    #region Menu & quit restart
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void DisplayMenu()
    {
        centerCursorImage.gameObject.SetActive(false);
        HideAllControlsInfos();
        if (RBPlayer.instance != null)
            RBPlayer.instance.canRotate = false;
        Cursor.lockState = CursorLockMode.None;
        GameMenuObject.SetActive(true);
    }

    public void DisplayWinPanel()
    {
        centerCursorImage.gameObject.SetActive(false);
        HideAllControlsInfos();
        Cursor.lockState = CursorLockMode.None;      
        WinPanel.SetActive(true);
    }

    public void HideMenu()
    {
        centerCursorImage.gameObject.SetActive(true);
        DisplayBaseControlsInfo();
        if (RBPlayer.instance != null) ;
        RBPlayer.instance.canRotate = true;
        Cursor.lockState = CursorLockMode.Locked;
        GameMenuObject.SetActive(false);
    }
    #endregion Menu & quit restart
}
