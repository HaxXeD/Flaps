using UnityEngine;
using DG.Tweening;

public class UITween : MonoBehaviour
{
    public Transform box;
    public CanvasGroup background;

    private Vector2 initBoxPos;

    private void Awake(){
        initBoxPos = box.localPosition;
    }

    private void OnEnable(){

        background.alpha = 0;
        background.DOFade(1,0.5f).SetUpdate(true);

        box.localPosition = new Vector2(0,-Screen.height);
        box.DOLocalMoveY(initBoxPos.y,0.5f).SetEase(Ease.OutExpo).SetDelay(0.1f).SetUpdate(true);
    }

    public void CloseSetting(){
        background.DOFade(0,0.5f).SetUpdate(true);
        box.DOLocalMoveY(-Screen.height,.5f).SetEase(Ease.InExpo).SetUpdate(true).OnComplete(()=>gameObject.SetActive(false));
    }
}

