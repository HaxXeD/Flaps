using UnityEngine.UI;
using System.Collections;
using UnityEngine;
using System;

public class FillScript : MonoBehaviour
{
    public event Action onFuelEmpty;
    Image fillBar;

    [Range(0.0f, 0.005f)]
    [SerializeField] float fillLossTime;

    private void Awake() => fillBar = GetComponent<Image>();
    // Start is called before the first frame update
    void Start()
    {
        fillBar.fillAmount = 1f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        StartCoroutine(ProgressDial());
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
}
