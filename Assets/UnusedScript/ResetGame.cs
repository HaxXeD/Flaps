using UnityEngine;

public class ResetGame : MonoBehaviour
{
    [SerializeField] PlaneSo[] planeSos;
    [SerializeField] BackgroundSO[] backgroundSOs;

    [SerializeField] PowerUpSO[] powerUpSos;

    public void Reset(){
        ResetPlane();
        ResetBackground();
        ResetPowerUPs();
        PlayerPrefs.DeleteAll();
    }

    void ResetPlane(){
        for(int i = 0; i<planeSos.Length;i++){
            planeSos[i].isPurchased = false;
            planeSos[i].isSelected = false;
        }

        planeSos[0].isPurchased = true;
        planeSos[0].isSelected = true;
    }

    void ResetBackground(){
        for(int i = 0; i<backgroundSOs.Length;i++){
            backgroundSOs[i].isPurchased = false;
            backgroundSOs[i].isSelected = false;
        }

        backgroundSOs[0].isPurchased = true;
        backgroundSOs[0].isSelected = true;
    }

    void ResetPowerUPs(){
        for(int i = 0; i<powerUpSos.Length;i++){
            powerUpSos[i].isPurchased = false;
        }
    }

    
}
