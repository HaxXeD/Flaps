using UnityEngine;
using UnityEngine.UI;

public class InitializeBackground : MonoBehaviour
{
    public BackgroundSO[] backgroundSOs;
    RawImage backgroundImage;

    void Start(){
        backgroundImage = GetComponent<RawImage>();
        for(int i = 0;i<backgroundSOs.Length;i++){
            if(!backgroundSOs[i].isSelected){
                continue;
            }
            else{
                backgroundImage.texture = backgroundSOs[i].backgroundImage;
            }

        }
    }
}
