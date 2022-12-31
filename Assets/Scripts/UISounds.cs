using UnityEngine;

public class UISounds : MonoBehaviour
{
    ListAudio listAudio;
    // Start is called before the first frame update
    void Start()
    {
        listAudio = FindObjectOfType<ListAudio>();
    }

    public void Click(){
        listAudio.PlayAudioWithOneShot(7);
    }
}
