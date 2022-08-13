using UnityEngine.UI;
using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Events;

public class FillScript : MonoBehaviour
{
    public event Action onFuelEmpty;
    public UnityEvent refillGauge = new UnityEvent();
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
        if(addingfill){
            StartCoroutine(ProgressDial());
        }
        else if(!addingfill){
            StartCoroutine(AddfillProgressDial());
        }
    }

    private IEnumerator ProgressDial()
    {        
         fillBar.fillAmount -= fillLossTime;
        
         //color change
        //  var green = new Color(0f, 1f, 0f, .5f);
        //  var red = new Color(1f, 0f, 0f, .5f);
        //  Color healthColor = Color.Lerp(red, green, fillBar.fillAmount);
        //  fillBar.color = healthColor;
        
         yield return null;
        
        if(fillBar.fillAmount==0)
            onFuelEmpty?.Invoke();   
    }

    private IEnumerator AddfillProgressDial(){
        fillBar.fillAmount+=0.005f;
        yield return null;

        if(fillBar.fillAmount==1)
            addingfill = true;
    }

    void OnRefill(){
        addingfill = false;
    }
}
