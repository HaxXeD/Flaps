using UnityEngine;
using DG.Tweening;
using System;

public class OnStartController : MonoBehaviour
{
    [SerializeField]Transform playerPlane,
                            enemyPlane,
                            title,
                            playBtn,
                            Others;

    [SerializeField] CanvasGroup darkBG;

    Vector2 othersPos,
            playBtnPos,
            titlePos;
    void Start()
    {
        // playerPlane.localPosition = new Vector2(-Screen.width-50f,playerPlane.position.y);
        // enemyPlane.localPosition = new Vector2(Screen.width+50f,enemyPlane.position.y);
        othersPos = Others.position;
        playBtnPos = playBtn.position;
        titlePos = title.position;    
        Others.position = new Vector2(othersPos.x,12);
        playBtn.position  = new Vector2(playBtnPos.x,-Screen.height);
        title.position = new Vector2(titlePos.x, Screen.height);
        darkBG.alpha = 0;
        MovePlanes();
        
    }

    private void MovePlanes()
    {
        playerPlane.DOLocalMoveX(15,1f).SetUpdate(true);
        enemyPlane.DOLocalMoveX(-15,1f).SetUpdate(true).OnComplete(()=>MakeBGDark());
    }

    private void MakeBGDark()
    {
        darkBG.DOFade(1,.5f).SetUpdate(true).OnComplete(()=>CenterTitlenPlay());
    }

    private void CenterTitlenPlay()
    {
        title.DOMove(titlePos,.5f).SetUpdate(true);
        playBtn.DOMove(playBtnPos,.5f).SetUpdate(true).OnComplete(()=>MoveOthers());
    }

    private void MoveOthers()
    {
        Others.DOMove(othersPos,.5f).SetUpdate(true);
    }
}
