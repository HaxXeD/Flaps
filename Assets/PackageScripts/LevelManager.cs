using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    SceneTransition sceneTransition;
    GameObject transitionImage;

    void Start(){
        sceneTransition = FindObjectOfType<SceneTransition>();
        transitionImage = sceneTransition.GetTransitionImage();
    }
    
    public void ChangeScene(int sceneIndex)
    {
        transitionImage.SetActive(true);
        transitionImage.GetComponent<CanvasGroup>().alpha = 0f;
        transitionImage.GetComponent<CanvasGroup>().DOFade(1f,.5f).SetUpdate(true).OnComplete(()=>{EndTransition(sceneIndex);});
    }

    private void EndTransition(int _index)
    {
        SceneManager.LoadScene(_index);
        transitionImage.GetComponent<CanvasGroup>().DOFade(0f,.5f).SetUpdate(true).OnComplete(()=>sceneTransition.GetTransitionImage().SetActive(false));
    }

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

