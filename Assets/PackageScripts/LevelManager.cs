using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    SceneTrasition sceneTrasition;
    GameObject trasitionImage;

    void Start(){
        sceneTrasition = FindObjectOfType<SceneTrasition>();
        trasitionImage = sceneTrasition.GetTrasitionImage();
    }
    
    public void ChangeScene(int sceneIndex)
    {
        // SceneManager.LoadScene(sceneIndex);
        trasitionImage.SetActive(true);
        trasitionImage.GetComponent<CanvasGroup>().alpha = 0f;
        // trasitionImage.transform.localPosition = new Vector2(-Screen.width,trasitionImage.transform.localPosition.y);
        trasitionImage.GetComponent<CanvasGroup>().DOFade(1f,.5f).SetUpdate(true).OnComplete(()=>{EndTrasition(sceneIndex);});
    }

    private void EndTrasition(int _index)
    {
        SceneManager.LoadScene(_index);
        // Time.timeScale = 1f;
        trasitionImage.GetComponent<CanvasGroup>().DOFade(0f,.5f).SetUpdate(true).OnComplete(()=>sceneTrasition.GetTrasitionImage().SetActive(false));
    }

    // IEnumerator SceneChange(int _index)
    // {
    //     yield return new WaitForSecondsRealtime(.5f);
    //     print("current index: " + _index);

        
    // }

    public void ReloadScene()
    {
        ChangeScene(ReturnCurrentSceneIndex());
    }

    public void NextScene(){
        ChangeScene(ReturnNextSceneIndex());
    }

    public int ReturnCurrentSceneIndex(){
        return SceneManager.GetActiveScene().buildIndex;
    }
    

    public int ReturnNextSceneIndex(){
        return ReturnCurrentSceneIndex()+1;
    }

    public void Home(){
        ChangeScene(0);
    }

}

