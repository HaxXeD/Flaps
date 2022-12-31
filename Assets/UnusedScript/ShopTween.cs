using UnityEngine;
using DG.Tweening;

public class ShopTween : MonoBehaviour
{
    [SerializeField]GameObject Main,Current;
    [SerializeField] CanvasGroup coinBox;
    void OnEnable()
    {
        Current.transform.localPosition = new Vector2(transform.localPosition.x,-Screen.height-500f);
        coinBox.DOFade(0,.2f).OnComplete(()=>{
            Main.transform.DOLocalMoveY(Screen.height + 500f ,.2f).SetEase(Ease.Linear).OnComplete(()=>{
                Main.SetActive(false);
                coinBox.DOFade(1,.5f);
            });
            Current.transform.DOLocalMoveY(0f,.2f);   
        });

        // transform.localPosition = new Vector2(Screen.width,transform.localPosition.y);
    }

    public void Back(){
        coinBox.DOFade(0,.2f).OnComplete(()=>{
            Main.SetActive(true);
            Main.transform.DOLocalMoveY(0f,.2f).OnComplete(()=>coinBox.DOFade(1,.5f));
            Current.transform.DOLocalMoveY(-Screen.height-500f,.2f).SetEase(Ease.Linear).OnComplete(()=>Current.SetActive(false));;
        });

    }

}
