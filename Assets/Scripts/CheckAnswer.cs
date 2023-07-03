using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CheckAnswer : MonoBehaviour
{
    ListAudio listAudio;
    QuestionSpawner questionSpawner;
    [SerializeField] FillScript fillScript;

    public void CheckAns(){
        if(transform.GetChild(0).GetComponent<TMP_Text>().text == questionSpawner.returnCorrectAnswer()){
            fillScript.refillGauge?.Invoke();
            ColorBlock _C = GetComponent<Button>().colors;
            _C.selectedColor = new Color(95,193,71);
            listAudio.PlayAudioWithOneShot(18);
            print("correct");
        }
        else{
            listAudio.PlayAudioWithOneShot(17);
            ColorBlock _C = GetComponent<Button>().colors;
            _C.selectedColor = new Color(255,103,128);
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
