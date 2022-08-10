using UnityEngine;
using DG.Tweening;

public class LevelTween : MonoBehaviour
{
    [SerializeField]GameObject Home,LevelPanel;
    [SerializeField]CanvasGroup background;
    void OnEnable()
    {
        LevelPanel.transform.localPosition = new Vector2(Screen.width+500,transform.localPosition.y);

        Home.transform.DOLocalMoveX(-Screen.width-500f,.5f).SetEase(Ease.Linear).OnComplete(()=>Home.SetActive(false));
        // transform.localPosition = new Vector2(Screen.width,transform.localPosition.y);
        LevelPanel.transform.DOLocalMoveX(0,.5f);
        background.DOFade(1,.5f);
    }

    public void Back(){
        Home.SetActive(true);


        Home.transform.DOLocalMoveX(0,.5f);
        LevelPanel.transform.DOLocalMoveX(Screen.width + 500f,.5f).SetEase(Ease.Linear);
        background.DOFade(0,.5f).OnComplete(()=>gameObject.SetActive(false));
        
    }

}