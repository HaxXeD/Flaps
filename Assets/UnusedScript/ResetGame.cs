using UnityEngine;

public class ResetGame : MonoBehaviour
{
    public void Reset(){
        PlayerPrefs.DeleteAll();
        FindObjectOfType<ColorSettings>().resetColour();
        FindObjectOfType<AudioManager>().ResetMusic();
    }    
}
