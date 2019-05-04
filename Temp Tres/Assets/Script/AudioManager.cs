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
    public static AudioManager instance;

    [Header("Aduio Mixer")]
    [SerializeField] private AudioMixer gameMixer;

    [Header("Current BGM Playing")]
    [SerializeField] private AudioSource backgroundMusic;

    [Header("Background Music")]
    [SerializeField] private AudioClip mainMenuBackground;
    [SerializeField] private AudioClip gameWaiting;
    [SerializeField] private AudioClip gameBackground;
    [SerializeField] private AudioClip victoryTheme;
    [SerializeField] private AudioClip defeatTheme;

    [Header("Sound Effect Reference")]
    [SerializeField] private AudioSource yourTurn;
    [SerializeField] private AudioSource userJoin;
    [SerializeField] private AudioSource userLeft;
    [SerializeField] private AudioSource dealCard;
    [SerializeField] private AudioSource chatMessage;

    [Header("Volume")]
    [SerializeField] private float masterVolume = 0;
    [SerializeField] private float soundEffectVolume = 0;
    [SerializeField] private float musicSliderVolume = 0;

    

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
    public static AudioManager Instance { get => instance; set => instance = value; }
    public AudioMixer GameMixer { get => gameMixer; set => gameMixer = value; }
    public AudioSource BackgroundMusic { get => backgroundMusic; set => backgroundMusic = value; }
    public AudioClip MainMenuBackground { get => mainMenuBackground; set => mainMenuBackground = value; }
    public AudioClip GameWaiting { get => gameWaiting; set => gameWaiting = value; }
    public AudioClip GameBackground { get => gameBackground; set => gameBackground = value; }
    public AudioClip VictoryTheme { get => victoryTheme; set => victoryTheme = value; }
    public AudioClip DefeatTheme { get => defeatTheme; set => defeatTheme = value; }
    public AudioSource YourTurn { get => yourTurn; set => yourTurn = value; }
    public AudioSource UserJoin { get => userJoin; set => userJoin = value; }
    public AudioSource UserLeft { get => userLeft; set => userLeft = value; }
    public AudioSource DealCard { get => dealCard; set => dealCard = value; }
    public AudioSource ChatMessage { get => chatMessage; set => chatMessage = value; }
    public float MasterVolume { get => masterVolume; set => masterVolume = value; }
    public float SoundEffectVolume { get => soundEffectVolume; set => soundEffectVolume = value; }
    public float MusicSliderVolume { get => musicSliderVolume; set => musicSliderVolume = value; }
    #endregion
}
