using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    [SerializeField]QuestionSpawner questionSpawner;
    Button UIButton;
    [SerializeField] private float DelayTime = .5f;
    void Start(){
        UIButton = GetComponent<Button>();
        UIButton.interactable = false;
        Invoke(nameof(SetPauseActiveWithDelay),DelayTime);
        questionSpawner.OnQuestionPanelActive += SetPauseButtonInactive;
        questionSpawner.OnQuestionPanelInactive += SetPauseButtonActive;
    }

    private void SetPauseButtonActive()
    {
        UIButton.interactable = true;
    }

    private void SetPauseButtonInactive()
    {
        UIButton.interactable = false;
    }

    private void SetPauseActiveWithDelay() => UIButton.interactable = true;

}
