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
    bool addingfill = false;
    private void Awake() => fillBar = GetComponent<Image>();
    // Start is called before the first frame update
    void Start()
    {
        fillBar.fillAmount = 1f;
        addingfill = true;
        refillGauge.AddListener(OnRefill);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(addingfill)StartCoroutine(ProgressDial());
        else if(!addingfill)StartCoroutine(AddfillProgressDial());
    }

    private IEnumerator ProgressDial()
    {        
        fillBar.fillAmount -= fillLossTime;
        yield return null;
        if(fillBar.fillAmount==0)OnFuelEmpty?.Invoke(false);   
    }

    private IEnumerator AddfillProgressDial(){
        fillBar.fillAmount+=0.005f;
        yield return null;

        if(fillBar.fillAmount==1)addingfill = true;
    }

    void OnRefill()=> addingfill = false;
}
