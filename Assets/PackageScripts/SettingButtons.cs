using UnityEngine;

public class SettingButtons : MonoBehaviour
{
    TimeScaleController timeScaleController;
    UIManager uiManager;
    void Start(){
        uiManager = FindObjectOfType<UIManager>();  
        timeScaleController = FindObjectOfType<TimeScaleController>();
    }
    public void ShowSettingsUI() => uiManager.GetSettingsUI().SetActive(true);
    public void ShowGameOverUI() => uiManager.GetGameOverUI().SetActive(true);
    public void ShowPauseUI(){
        timeScaleController.OnPause.Invoke();
        uiManager.GetPauseUI().SetActive(true);
    }
    public void HideSettingsUI() => uiManager.GetSettingsUI().GetComponent<UITween>().CloseSetting();
    public void HideGameOverUI() => uiManager.GetGameOverUI().GetComponent<UITween>().CloseSetting();
    public void HidePauseUI(){
        uiManager.GetPauseUI().GetComponent<UITween>().CloseSetting();
        timeScaleController.OnUnpause.Invoke();
    }
}
