using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] AudioClip[] gameSounds;
    AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    public void PlayTutorial()
    {
        audioSource.clip = gameSounds[0];
        audioSource.Play();
    }

  
    public void PlayOrder(int num)
    {
        audioSource.clip = gameSounds[num];
        audioSource.Play();
    }
}
