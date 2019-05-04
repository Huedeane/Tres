using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    #region Variable
    [SerializeField] private Slider m_MasterSlider;
    [SerializeField] private Slider m_MusicSlider;
    [SerializeField] private Slider m_SoundEffectSlider;
    #endregion

    public void SetSlider()
    {
        MasterSlider.value = AudioManager.Instance.MasterVolume;
        SoundEffectSlider.value = AudioManager.Instance.SoundEffectVolume;
        MusicSlider.value = AudioManager.Instance. MusicSliderVolume;
    }

    public void SetMasterVolume(float volume)
    {
        AudioManager.Instance.MasterVolume = volume;

        AudioManager.Instance.GameMixer.SetFloat("Master", volume);
        if (MasterSlider.value == -30)
        {
            AudioManager.Instance.GameMixer.SetFloat("Master", -80);
        }
    }

    public void SetMusicVolume(float volume)
    {
        AudioManager.Instance.MusicSliderVolume = volume;

        AudioManager.Instance.GameMixer.SetFloat("Music", volume);
        if (MusicSlider.value == -30)
        {
            AudioManager.Instance.GameMixer.SetFloat("Music", -80);
        }
    }

    public void SetSoundEffectsVolume(float volume)
    {
        AudioManager.Instance.SoundEffectVolume = volume;

        AudioManager.Instance.GameMixer.SetFloat("Sound Effect", volume);
        if (SoundEffectSlider.value == -30)
        {
            AudioManager.Instance.GameMixer.SetFloat("Sound Effect", -80);
        }
    }


    #region Getter & Setter
    public Slider MasterSlider { get => m_MasterSlider; set => m_MasterSlider = value; }
    public Slider MusicSlider { get => m_MusicSlider; set => m_MusicSlider = value; }
    public Slider SoundEffectSlider { get => m_SoundEffectSlider; set => m_SoundEffectSlider = value; }
    #endregion
}
