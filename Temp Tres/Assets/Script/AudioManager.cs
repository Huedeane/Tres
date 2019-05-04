using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public enum E_BackGroundMusic { MainMenuBackground, GameWaiting, GameBackground, VictoryTheme, DefeatTheme }
public enum E_SoundEffect { YourTurn, UserJoin, UserLeft, DealCard, ChatMessage }

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Aduio Mixer")]
    [SerializeField] private AudioMixer m_GameMixer;

    [Header("Current BGM Playing")]
    [SerializeField] private AudioSource m_BackgroundMusic;

    [Header("Background Music")]
    [SerializeField] private AudioClip m_MainMenuBackground;
    [SerializeField] private AudioClip m_GameWaiting;
    [SerializeField] private AudioClip m_GameBackground;
    [SerializeField] private AudioClip m_VictoryTheme;
    [SerializeField] private AudioClip m_DefeatTheme;

    [Header("Sound Effect Reference")]
    [SerializeField] private AudioSource m_YourTurn;
    [SerializeField] private AudioSource m_UserJoin;
    [SerializeField] private AudioSource m_UserLeft;
    [SerializeField] private AudioSource m_DealCard;
    [SerializeField] private AudioSource m_ChatMessage;

    [Header("Volume")]
    [SerializeField] private float m_MasterVolume = 0;
    [SerializeField] private float m_SoundEffectVolume = 0;
    [SerializeField] private float m_MusicSliderVolume = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        switch (scene.name)
        {
            case "Main Menu":
                Instance.PlayBackgroundMusic(E_BackGroundMusic.MainMenuBackground);
                break;
            case "Game":
                Instance.PlayBackgroundMusic(E_BackGroundMusic.GameWaiting);
                break;
        }
    }

    public void PlayBackgroundMusic(E_BackGroundMusic backGroundMusic)
    {
        if (BackgroundMusic.isPlaying)
            BackgroundMusic.Stop();

        switch (backGroundMusic)
        {
            case E_BackGroundMusic.MainMenuBackground:
                BackgroundMusic.clip = MainMenuBackground;
                BackgroundMusic.loop = true;
                break;
            case E_BackGroundMusic.GameWaiting:
                BackgroundMusic.clip = GameWaiting;
                BackgroundMusic.loop = true;
                break;
            case E_BackGroundMusic.GameBackground:
                BackgroundMusic.clip = GameBackground;
                BackgroundMusic.loop = true;
                break;
            case E_BackGroundMusic.VictoryTheme:
                BackgroundMusic.clip = VictoryTheme;
                BackgroundMusic.loop = false;
                break;
            case E_BackGroundMusic.DefeatTheme:
                BackgroundMusic.clip = DefeatTheme;
                BackgroundMusic.loop = false;
                break;
        }

        BackgroundMusic.Play();
    }

    public void PlaySoundEffect(E_SoundEffect soundEffect)
    {
        switch (soundEffect)
        {
            case E_SoundEffect.ChatMessage:
                ChatMessage.Play();
                break;
            case E_SoundEffect.DealCard:
                DealCard.Play();
                break;
            case E_SoundEffect.UserJoin:
                UserJoin.Play();
                break;
            case E_SoundEffect.UserLeft:
                UserLeft.Play();
                break;
            case E_SoundEffect.YourTurn:
                YourTurn.Play();
                break;
        }
    }



    #region Getter & Setter
    public AudioMixer GameMixer { get => m_GameMixer; set => m_GameMixer = value; }
    public AudioSource BackgroundMusic { get => BackgroundMusic; set => BackgroundMusic = value; }
    public AudioClip MainMenuBackground { get => m_MainMenuBackground; set => m_MainMenuBackground = value; }
    public AudioClip GameWaiting { get => m_GameWaiting; set => m_GameWaiting = value; }
    public AudioClip GameBackground { get => m_GameBackground; set => m_GameBackground = value; }
    public AudioClip VictoryTheme { get => m_VictoryTheme; set => m_VictoryTheme = value; }
    public AudioClip DefeatTheme { get => m_DefeatTheme; set => m_DefeatTheme = value; }
    public AudioSource YourTurn { get => m_YourTurn; set => m_YourTurn = value; }
    public AudioSource UserJoin { get => m_UserJoin; set => m_UserJoin = value; }
    public AudioSource UserLeft { get => m_UserLeft; set => m_UserLeft = value; }
    public AudioSource DealCard { get => m_DealCard; set => m_DealCard = value; }
    public AudioSource ChatMessage { get => m_ChatMessage; set => m_ChatMessage = value; }
    public float MasterVolume { get => m_MasterVolume; set => m_MasterVolume = value; }
    public float SoundEffectVolume { get => m_SoundEffectVolume; set => m_SoundEffectVolume = value; }
    public float MusicSliderVolume { get => m_MusicSliderVolume; set => m_MusicSliderVolume = value; }    
    #endregion
}
