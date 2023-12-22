using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uicontroller : MonoBehaviour
{
    public Slider musicslider, sfxslider;
    public void ToggleMusic()
    {
        audio1.Instance.ToggleMusic();
    }
    public void ToggleSFX()
    {
        audio1.Instance.ToggleSFX();
    }
    public void MusicVolume()
    {
        audio1.Instance.MusicVolume(musicslider.value); 
    }
    public void SFXVolume()
    {
        audio1.Instance.SFXVolume(sfxslider.value);
    }
}
