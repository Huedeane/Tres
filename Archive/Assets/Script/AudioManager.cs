using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum E_BackGroundMusic { Main_Menu_Background, Main_Game_Waiting, Main_Game_Background, Victory_Theme, Defeat_Theme }

public enum E_SoundEffect { Your_Turn, User_Join, User_Left, Deal_Card, Chat_Message }

public class AudioManager : MonoBehaviour
{
    [SerializeField] public static AudioManager Instance;

    [Header("Music Reference")]
    public AudioSource MainMenuBackground;
    public AudioSource MainGameWaiting;
    public AudioSource MainGameBackground;
    public AudioSource VictoryTheme;
    public AudioSource DefeatTheme;

    [Header("Sound Effect Reference")]
    public AudioSource YourTurn;
    public AudioSource UserJoin;
    public AudioSource UserLeft;
    public AudioSource DealCard;
    public AudioSource ChatMessage;

    [Header("Current Audio Playing")]
    public AudioSource BackgroundMusic;
    public AudioSource SoundEffect;

    [Header("Volume")]
    public float masterVolume = -15;
    public float soundEffectVolume = -15;
    public float backgroundSliderVolume = -15;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnSceneUnloaded(Scene scene)
    {
        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        switch (scene.name)
        {
            case "Main Menu":
                Instance.ChangeBackground(E_BackGroundMusic.Main_Menu_Background);
                break;
            case "Main Game":
                Instance.ChangeBackground(E_BackGroundMusic.Main_Game_Waiting);
                break;
        }
    }

    public void ChangeBackground(E_BackGroundMusic background)
    {

        if(BackgroundMusic.isPlaying)
            BackgroundMusic.Stop();

        switch (background)
        {
            case E_BackGroundMusic.Main_Menu_Background:
                BackgroundMusic = MainMenuBackground;
                break;
            case E_BackGroundMusic.Main_Game_Waiting:
                BackgroundMusic = MainGameWaiting;
                break;
            case E_BackGroundMusic.Main_Game_Background:
                BackgroundMusic = MainGameBackground;
                break;
            case E_BackGroundMusic.Victory_Theme:
                BackgroundMusic = VictoryTheme;
                break;
            case E_BackGroundMusic.Defeat_Theme:
                BackgroundMusic = DefeatTheme;                
                break;
        }

        BackgroundMusic.Play();
    }

    public void PlaySoundEffect(E_SoundEffect soundEffect)
    {

        switch (soundEffect)
        {
            case E_SoundEffect.Chat_Message:
                if (ChatMessage.isPlaying)
                    ChatMessage.Stop();
                ChatMessage.Play();
                break;
            case E_SoundEffect.Deal_Card:
                if (DealCard.isPlaying)
                    ChatMessage.Stop();
                DealCard.Play();              
                break;
            case E_SoundEffect.User_Join:
                if (UserJoin.isPlaying)
                    ChatMessage.Stop();
                UserJoin.Play();
                break;
            case E_SoundEffect.User_Left:
                if (UserLeft.isPlaying)
                    ChatMessage.Stop();
                UserLeft.Play();
                break;
            case E_SoundEffect.Your_Turn:
                if (YourTurn.isPlaying)
                    ChatMessage.Stop();
                YourTurn.Play();
                break;
        }
    }







}
