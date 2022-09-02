using UnityEngine;
using TMPro;


public class CheckAnswer : MonoBehaviour
{
    ListAudio listAudio;
    Spawner spawner;
    [SerializeField] FillScript fillScript;
    public void CheckAns(){
        if(transform.GetChild(0).GetComponent<TMP_Text>().text == spawner.returnCorrectAnswer()){
            fillScript.refillGauge?.Invoke();
            listAudio.PlayAudioWithOneShot(18);
            print("correct");
        }
        else{
            listAudio.PlayAudioWithOneShot(17);
            print("incorrect");
        }

        spawner.AddFuel();
    }
    // Start is called before the first frame update
    void Start()
    {
        spawner = FindObjectOfType<Spawner>();
        listAudio = FindObjectOfType<ListAudio>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
