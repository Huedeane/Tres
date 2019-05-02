using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer AudioControl;
    public Slider MasterSlider;
    public Slider SoundEffectSlider;
    public Slider BackgroundSlider;

    public NetworkManager netMan;

    public void ExitGame()
    {
        Destroy(netMan.gameObject);
        SceneManager.LoadScene("Main Menu");       
    }

    public void SetMasterVolume(float volume)
    {
        AudioControl.SetFloat("Master", volume);
        if (MasterSlider.value == -30)
        {
            AudioControl.SetFloat("Master", -80);
        }
    }

    public void SetSoundEffectsVolume(float volume)
    {
        AudioControl.SetFloat("Sound Effect", volume);
        if (SoundEffectSlider.value == -30)
        {
            AudioControl.SetFloat("Sound Effect", -80);
        }
    }
    public void SetBackgroundVolume(float volume)
    {
        AudioControl.SetFloat("Background", volume);
        if (BackgroundSlider.value == -30)
        {
            AudioControl.SetFloat("Background", -80);
        }
    }
}
