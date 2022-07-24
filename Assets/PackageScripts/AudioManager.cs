using System;
using System.Collections;
using System.Collections.Generic;
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
        
        if (PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1) == 1)
        {
            Debug.Log("First Time Opening");

            //Set first time opening to false
            PlayerPrefs.SetInt("FIRSTTIMEOPENING", 0);
            gameMusic.volume = musicSlider.value = 0.5f;
            gameSound.volume = soundSlider.value = 1f;
            
           
        }
        else
        {
            Debug.Log("NOT First Time Opening");
            gameMusic.volume = musicSlider.value = PlayerPrefs.GetFloat("Music");
            gameSound.volume = soundSlider.value = PlayerPrefs.GetFloat("Sound");

        }
    }

    private void Update()
    {
        SetVolume();
    }

    private void SetVolume()
    {
        gameMusic.volume = musicSlider.value;
        gameSound.volume = soundSlider.value;     
    }


    public void OnDestroy()
    {
        PlayerPrefs.SetFloat("Music", musicSlider.value);
        PlayerPrefs.SetFloat("Sound", soundSlider.value);
    }


}
