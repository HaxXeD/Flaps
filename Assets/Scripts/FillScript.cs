using UnityEngine.UI;
using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Events;

public class FillScript : MonoBehaviour
{
    public event Action<bool> OnFuelEmpty;
    [HideInInspector] public UnityEvent refillGauge = new UnityEvent();
    TutorialManager tutorialManager;
    CheckAnswer checkAnswer;
    Image fillBar;

    [Range(0.0f, 0.005f)]
    [SerializeField] float fillLossTime;
    private bool addingfill = true;
    private bool startReducingFill = false;
    private float maxFill = 1f;
    private void Awake()
    {
        fillBar = GetComponent<Image>();
        fillBar.fillAmount = maxFill;
        refillGauge.AddListener(OnRefill);
        Invoke(nameof(DelayAwake),.5f);
    }

    private void DelayAwake()
    {
        startReducingFill = true;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if(addingfill && startReducingFill)StartCoroutine(ProgressDial());
        else if(!addingfill)StartCoroutine(AddfillProgressDial());
    }

    private IEnumerator ProgressDial()
    {        
        fillBar.fillAmount -= fillLossTime;
        yield return null;
        if(fillBar.fillAmount==0){
            OnFuelEmpty?.Invoke(false);  
        } 
    }

    private IEnumerator AddfillProgressDial(){
        fillBar.fillAmount+=0.005f;
        yield return null;

        if(fillBar.fillAmount==1)addingfill = true;
    }

    void OnRefill()=> addingfill = false;
}
