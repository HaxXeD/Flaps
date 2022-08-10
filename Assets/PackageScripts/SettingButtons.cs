using UnityEngine;
using TMPro;

public class SettingButtons : MonoBehaviour
{
    public event System.Action OnPause,OnUnpause;
    UIManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();  
    }

    public void ShowSettingsUI()
    {
        uiManager.GetSettingsUI().SetActive(true);
    }

    public void ShowGameOverUI(){
        uiManager.GetGameOverUI().SetActive(true);
        // Time.timeScale = 0f;
    }

    // public void ShowLeveLStartUI(){
    //     uiManager.GetLevelStartUI().SetActive(true);
    // }

    // public void ShowLevelCompleteUI(){
    //     uiManager.GetLevelCompleteUI().SetActive(true);
    // }

    public void ShowPauseUI(){
        uiManager.GetPauseUI().SetActive(true);
        OnPause?.Invoke();
    }
    // public void ShowQuestionUI(){
    //     uiManager.GetQuestionUI().SetActive(true);
    //     Time.timeScale = 0f;
    // }
    // public void HideQuestionUI()
    // {
    //     uiManager.GetQuestionUI().GetComponent<UITween>().CloseSetting();
    //     Time.timeScale = 1f;
    // }

    // public void CheckAnwer(){
    //     OnClicked?.Invoke(gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text);
    // }

    public void HideSettingsUI()
    {
        uiManager.GetSettingsUI().GetComponent<UITween>().CloseSetting();
    }
    public void HideGameOverUI()
    {
        uiManager.GetGameOverUI().GetComponent<UITween>().CloseSetting();
        // Time.timeScale = 1f;
    }    
    
    // public void HideLevelCompleteUI(){
    //     uiManager.GetLevelCompleteUI().GetComponent<UITween>().CloseSetting();
    // }
    public void HidePauseUI()
    {
        // FindObjectOfType<PlayerMovement>().StartEaseUp();
        // OnUnpause?.Invoke();
        FindObjectOfType<PlayerMovement>().OnUnpause.Invoke();

        uiManager.GetPauseUI().GetComponent<UITween>().CloseSetting();
    }
}
