using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio1 : MonoBehaviour
{
    public static audio1 Instance;
    public sound1[] musicsounds, sfxsounds;
    public AudioSource musicsource, sfxsource;


    public void Awake()
    {

        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    public void Start()
    {
        PlayMusic("");
    }
    public void PlayMusic(string name)
    {
        sound1 s = Array.Find(musicsounds, (x) => x.name == name);
        if(s != null)
        {
            Debug.Log("sound not found");
        }
        else
        {
            musicsource.clip = s.clip;
            musicsource.Play();

        }

    }
    public void PlaySfx(string name)
    {
        sound1 s = Array.Find(musicsounds, (x) => x.name == name);
        if (s != null)
        {
            Debug.Log("sound not found");
        }
        else
        {
            sfxsource.PlayOneShot(s.clip);
            

        }

    }
    public void ToggleMusic()
    {
        musicsource.mute= !musicsource.mute;
    }
    public void ToggleSFX()
    {
        sfxsource.mute= !sfxsource.mute;    
    }
    public void MusicVolume(float volume)

    {
        musicsource.volume = volume; 
    }
    public void SFXVolume(float volume)
    {
         sfxsource.volume = volume;
    }
}