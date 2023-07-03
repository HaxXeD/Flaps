using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject SettingsUI,PauseUI,GameOverUI;

    [SerializeField] Slider brightnessSlider,saturationSlider,contrastSlider;
    [SerializeField] TMP_Text currentScoreTxt,highScoreTxt;

    Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        GetCamera();
    }


    private void Update()
    {
        if (canvas.worldCamera == null)
        {
            GetCamera();
        }
    }
    public void GetCamera()
    {
        canvas.worldCamera = Camera.main;
    }

    public GameObject GetSettingsUI()
    {
        return SettingsUI;
    }

    public GameObject GetPauseUI(){
        return PauseUI;
    }
    //     public GameObject GetQuestionUI(){
    //     return QuestionUI;
    // }

    public GameObject GetGameOverUI(){
        return GameOverUI;
    }

    // public GameObject GetLevelCompleteUI(){
    //     return LevelCompleteUI;
    // }

    public Slider GetBrightness()
    {
        
        return brightnessSlider;
    }

    public Slider GetContrast()
    {
        return contrastSlider;
    }

    public Slider GetSaturation()
    {
        return saturationSlider;
    }

    public TMP_Text GetCurrentScore(){
        return currentScoreTxt;
    }

    public TMP_Text GetHighScore(){
        return highScoreTxt;
    }
}
