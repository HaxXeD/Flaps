using UnityEngine;
using DG.Tweening;

public class LevelTween : MonoBehaviour
{
    [SerializeField]GameObject Home,LevelPanel;
    [SerializeField]CanvasGroup background;
    void OnEnable()
    {
        LevelPanel.transform.localPosition = new Vector2(Screen.width,transform.localPosition.y);
        Home.transform.DOLocalMoveX(-Screen.width,.5f).SetEase(Ease.Linear).OnComplete(()=>Home.SetActive(false));
        // transform.localPosition = new Vector2(Screen.width,transform.localPosition.y);
        LevelPanel.transform.DOMove(Vector2.zero,.5f);   
        background.DOFade(1,1f);
    }

    public void Back(){
        Home.SetActive(true);
        Home.transform.DOMove(Vector2.zero,1f);
        LevelPanel.transform.DOLocalMoveX(Screen.width,.5f).SetEase(Ease.Linear);
        background.DOFade(0,1f).OnComplete(()=>gameObject.SetActive(false));
    }

}