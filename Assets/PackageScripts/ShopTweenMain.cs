using UnityEngine;
using DG.Tweening;

public class ShopTweenMain : MonoBehaviour
{
    [SerializeField]GameObject Home,LevelPanel;
    [SerializeField]CanvasGroup background,coinBox;
    void OnEnable()
    {
        LevelPanel.transform.localPosition = new Vector2(Screen.width+500,transform.localPosition.y);
        coinBox.alpha = 0f;
        Home.transform.DOLocalMoveX(-Screen.width-500f,.5f).SetEase(Ease.Linear).OnComplete(()=>Home.SetActive(false));
        // transform.localPosition = new Vector2(Screen.width,transform.localPosition.y);
        LevelPanel.transform.DOLocalMoveX(0,.5f).OnComplete(()=>coinBox.DOFade(1,.2f));
        background.DOFade(1,.5f);
    }

    public void Back(){
        Home.SetActive(true);

        coinBox.DOFade(0,.2f).OnComplete(()=>{
        Home.transform.DOLocalMoveX(0,.5f);
        LevelPanel.transform.DOLocalMoveX(Screen.width + 500f,.5f).SetEase(Ease.Linear);
        background.DOFade(0,.5f).OnComplete(()=>gameObject.SetActive(false));
        });
    }

}