using UnityEngine;

public class InitializePlayerTempScript : MonoBehaviour
{
    // Start is called before the first frame update    
    public PlaneSo[] planeSos;
    SpriteRenderer planeSprite;

    void Awake(){
        planeSprite = GetComponent<SpriteRenderer>();
        for(int i = 0;i<planeSos.Length;i++){
            if(!planeSos[i].isSelected){
                continue;
            }
            else{
                planeSprite.sprite = planeSos[i].planeImage;
            }

        }
    }
}
