using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    #region Variable
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundEffectSlider;
    #endregion

    public void SetSlider()
    {
        MasterSlider.value = AudioManager.instance.MasterVolume;
        SoundEffectSlider.value = AudioManager.instance.SoundEffectVolume;
        MusicSlider.value = AudioManager.instance. MusicSliderVolume;
    }

    public void SetMasterVolume(float volume)
    {
        AudioManager.instance.MasterVolume = volume;

        AudioManager.instance.GameMixer.SetFloat("Master", volume);
        if (MasterSlider.value == -30)
        {
            AudioManager.instance.GameMixer.SetFloat("Master", -80);
        }
    }

    public void SetMusicVolume(float volume)
    {
        AudioManager.instance.MusicSliderVolume = volume;

        AudioManager.instance.GameMixer.SetFloat("Music", volume);
        if (MusicSlider.value == -30)
        {
            AudioManager.instance.GameMixer.SetFloat("Music", -80);
        }
    }

    public void SetSoundEffectsVolume(float volume)
    {
        AudioManager.instance.SoundEffectVolume = volume;

        AudioManager.instance.GameMixer.SetFloat("Sound Effect", volume);
        if (SoundEffectSlider.value == -30)
        {
            AudioManager.instance.GameMixer.SetFloat("Sound Effect", -80);
        }
    }
    

    #region Getter & Setter
    public Slider MasterSlider { get => masterSlider; set => masterSlider = value; }
    public Slider MusicSlider { get => musicSlider; set => musicSlider = value; }
    public Slider SoundEffectSlider { get => soundEffectSlider; set => soundEffectSlider = value; }
    #endregion
}
