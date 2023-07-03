using UnityEngine;
using TMPro;

public class SettingButtons : MonoBehaviour
{
    TimeScaleController timeScaleController;
    UIManager uiManager;
    void Start(){
        uiManager = FindObjectOfType<UIManager>();  
    }
    public void ShowSettingsUI() => uiManager.GetSettingsUI().SetActive(true);
    public void ShowGameOverUI() => uiManager.GetGameOverUI().SetActive(true);
    public void ShowPauseUI(){
        FindAnyObjectByType<TimeScaleController>().OnPause.Invoke();
        uiManager.GetPauseUI().SetActive(true);
    }
    public void HideSettingsUI() => uiManager.GetSettingsUI().GetComponent<UITween>().CloseSetting();
    public void HideGameOverUI() => uiManager.GetGameOverUI().GetComponent<UITween>().CloseSetting();
    public void HidePauseUI(){
        FindAnyObjectByType<TimeScaleController>().OnUnpause.Invoke();
        uiManager.GetPauseUI().GetComponent<UITween>().CloseSetting();
    }

    public void SetCurrentScore(string currentScore) => uiManager.GetCurrentScore().text = currentScore;
    public void SetHighScore(string highScore) => uiManager.GetHighScore().text = highScore;
}
