using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource gameMusic;
    [SerializeField] AudioSource gameSound;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundSlider;

    private void Start()
    {
        
        if (PlayerPrefs.GetInt("FIRSTTIMEOPENINGMUSIC", 1) == 1)
        {
            Debug.Log("First Time Opening Music");

            //Set first time opening to false
            PlayerPrefs.SetInt("FIRSTTIMEOPENINGMUSIC", 0);
            ResetMusic();
        }
        else
        {
            Debug.Log("NOT First Time Opening");
            gameMusic.volume = musicSlider.value = PlayerPrefs.GetFloat("Music");
            gameSound.volume = soundSlider.value = PlayerPrefs.GetFloat("Sound");

        }

        musicSlider.onValueChanged.AddListener(delegate{SetMusic();});
        soundSlider.onValueChanged.AddListener(delegate{SetSFX();});
    }

    public void ResetMusic()
    {
        gameMusic.volume = musicSlider.value = 0.5f;
        gameSound.volume = soundSlider.value = 1f;
    }

    private void SetSFX(){
        gameSound.volume = soundSlider.value;
        PlayerPrefs.SetFloat("Sound",soundSlider.value);
    }

    private void SetMusic(){
        gameMusic.volume = musicSlider.value;
        PlayerPrefs.SetFloat("Music",musicSlider.value);
    }
}
