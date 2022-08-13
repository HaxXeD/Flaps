using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class CheckAnswer : MonoBehaviour
{
    Spawner spawner;
    [SerializeField] FillScript fillScript;
    public void CheckAns(){
        if(transform.GetChild(0).GetComponent<TMP_Text>().text == spawner.returnCorrectAnswer()){
            fillScript.refillGauge?.Invoke();
            print("correct");
        }
        else{
            print("incorrect");
        }

        spawner.AddFuel();
    }
    // Start is called before the first frame update
    void Start()
    {
        spawner = FindObjectOfType<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
