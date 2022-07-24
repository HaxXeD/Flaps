using System.Collections.Generic;
using UnityEngine;

public class ListAudio : MonoBehaviour
{
    [SerializeField] List<AudioClip> gameSounds = new List<AudioClip>();
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(int _index)
    {
        audioSource.clip = gameSounds[_index];
        audioSource.Play();
    }

    public void PlayAudioWithOneShot(int _index){
        audioSource.clip = gameSounds[_index];
        audioSource.PlayOneShot(audioSource.clip);
    }

}
