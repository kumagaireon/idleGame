using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip musicClip;

    void Start()
    {
        if(audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        audioSource.clip = musicClip;

        PlayMusic();
    }

    public void PlayMusic()
    {
        if(audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        if(audioSource!=null && !audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void PauseMusic()
    {
        if(audioSource != null && audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }
        
}
