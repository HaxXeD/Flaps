using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialManager : MonoBehaviour
{
    LevelManager levelManager;
    [SerializeField] TMP_Text dialogue;
    [SerializeField] Image displayImage;
    [SerializeField] string[] tutorialDialogues;
    [SerializeField] Sprite[] tutorialImages;

    [SerializeField] GameObject fillGauge;
    [SerializeField] GameObject fillScript;
    int index;
    int imageIndex;
    [SerializeField] float typingSpeed;

    Vector2 imagePosition;

    ListAudio listAudio;
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        imagePosition = displayImage.transform.position;
        dialogue.text = null;
        listAudio = FindObjectOfType<ListAudio>();
        listAudio.PlayAudioWithOneShot(8);
        displayImage.sprite = tutorialImages[imageIndex];
        displayImage.transform.position = new Vector2(-Screen.width,imagePosition.y);
        displayImage.transform.DOLocalMoveX(0,3f);
        StartCoroutine(Types());
    }

    IEnumerator Types()
    {
        foreach(char letter in tutorialDialogues[index].ToCharArray()){
            dialogue.text += letter;
            if(dialogue.text == tutorialDialogues[index])
            {
                Invoke("NewMethod",2f);
            }
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void NewMethod()
    {
        if (index < tutorialDialogues.Length - 1)
        {
            index++;
            dialogue.text = "";
            if(imageIndex<tutorialImages.Length - 1){
                displayImage.transform.localScale = Vector2.one;
                imageIndex++;
                displayImage.sprite = tutorialImages[imageIndex];

                switch(imageIndex){
                    case 1:
                        displayImage.transform.DOJump(imagePosition,1.5f,1,1f).SetEase(Ease.InSine).SetDelay(3f).OnComplete(()=>{
                            displayImage.transform.DOLocalMoveX(Screen.width,1f).SetDelay(1f);
                        });
                    break;
                    case 2:
                        displayImage.transform.DOLocalMoveX(0,1f).OnComplete(()=>
                            displayImage.transform.DOScale(Vector2.one*2f,0.5f));
                    break;
                    case 3:
                        displayImage.transform.gameObject.SetActive(true);
                        fillGauge.SetActive(false);
                    break;
                    case 4:
                        imageIndex = index-2;
                        StartCoroutine(ChangePowerUps());
                    break;
                }

                switch(index){
                    case 3:
                        displayImage.transform.gameObject.SetActive(false);
                        imageIndex = 1;
                        fillGauge.SetActive(true);
                    break;
                    case 4:
                        fillScript.GetComponent<FillScript>().refillGauge?.Invoke();
                    break;
                }
            }
            else if(imageIndex>=tutorialImages.Length - 1) {
                displayImage.enabled = false;
            }
            print(index);

            listAudio.PlayAudioWithOneShot(8+index);
            StartCoroutine(Types());
        }
        else
        {
            Invoke("GoHome",1.5f);
        }
    }

    void GoHome(){
        levelManager.Home();
    }
    

    IEnumerator ChangePowerUps(){
        if(imageIndex< tutorialImages.Length - 1){
            yield return new WaitForSeconds(.8f);
            imageIndex++;
            displayImage.sprite  = tutorialImages[imageIndex];
            StartCoroutine(ChangePowerUps());
        }
    }

}
