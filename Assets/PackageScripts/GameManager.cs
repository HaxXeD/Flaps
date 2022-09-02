using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private DateTime _sessionStartTime;
    private DateTime _sessionEndTime;

    [SerializeField]
    PlaneSo[] planes;
    
    [SerializeField]
    PowerUpSO[] powers;

    [SerializeField]
    BackgroundSO[] backgrounds;

    void Start() {
        
        PlaneData planeData = SaveSystem.LoadPlane();
        if(planeData!=null){
            for (int i = 0;i<planes.Length;i++){
                planes[i].isPurchased = planeData.isPurchased[i];
                planes[i].isSelected = planeData.isSelected[i];
            }
        }

        CollectableData collectableData = SaveSystem.LoadPowers();
        if(collectableData!= null){
            for (int i = 0;i<powers.Length;i++){
                powers[i].isPurchased = collectableData.isPurchased[i];
            }
        }

        BackgroundData backgroundData = SaveSystem.LoadBackground(backgrounds);
        if(backgroundData!=null){
            for (int i = 0;i<backgrounds.Length;i++){
                backgrounds[i].isPurchased = backgroundData.isPurchased[i];
                backgrounds[i].isSelected = backgroundData.isSelected[i];
            }
        }

        _sessionStartTime = DateTime.Now;
        Debug.Log(
        "Game session start @: " + DateTime.Now);
    }
    void OnApplicationQuit() {
        _sessionEndTime = DateTime.Now;
        TimeSpan timeDifference =
        _sessionEndTime.Subtract(_sessionStartTime);
        Debug.Log(
        "Game session ended @: " + DateTime.Now);
        Debug.Log(
        "Game session lasted: " + timeDifference);

        SaveSystem.SavePlane(planes);
        SaveSystem.SaveCollectable(powers);
        SaveSystem.SaveBackground(backgrounds);
    }
}
