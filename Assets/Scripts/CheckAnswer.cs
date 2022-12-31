using UnityEngine;
using TMPro;

public class CheckAnswer : MonoBehaviour
{
    ListAudio listAudio;
    QuestionSpawner questionSpawner;
    [SerializeField] FillScript fillScript;
    public void CheckAns(){
        if(transform.GetChild(0).GetComponent<TMP_Text>().text == questionSpawner.returnCorrectAnswer()){
            fillScript.refillGauge?.Invoke();
            listAudio.PlayAudioWithOneShot(18);
            print("correct");
        }
        else{
            listAudio.PlayAudioWithOneShot(17);
            print("incorrect");
        }

        questionSpawner.SetQuestionInactive();
    }
    // Start is called before the first frame update
    void Start()
    {
        questionSpawner = FindObjectOfType<QuestionSpawner>();
        listAudio = FindObjectOfType<ListAudio>();
    }
}
