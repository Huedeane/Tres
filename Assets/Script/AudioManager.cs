using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum E_BackGroundMusic { Main_Menu_Background, Main_Game_Waiting, Main_Game_Background}

public class AudioManager : MonoBehaviour
{
    [SerializeField] private static AudioManager audioManager;

    [Header("Audio Source Reference")]
    public AudioSource MainMenuBackground;
    public AudioSource MainGameWaiting;
    public AudioSource MainGameBackground;

    [Header("Current Audio Playing")]
    public AudioSource BackgroundMusic;
    public AudioSource SoundEffect;
    

    public static AudioManager Instance
    {
        get
        {
            if (!audioManager)
            {
                audioManager = FindObjectOfType(typeof(AudioManager)) as AudioManager;

                if (!audioManager)
                {
                    Debug.LogError("Error: There is no active Game Manager attached to a gameObject");
                }
                else
                {
                    audioManager.Init();
                }
            }

            return audioManager;
        }
        set
        {
            audioManager = value;
        }
    }

    private void Awake()
    {
        Instance = Instance;
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    
    private void Init()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void OnSceneUnloaded(Scene scene)
    {
        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.Log(scene.name);
        switch (scene.name)
        {
            case "Main Menu":
                Debug.Log(scene.name);
                ChangeBackground(E_BackGroundMusic.Main_Menu_Background);
                break;
            case "Main Game":
                ChangeBackground(E_BackGroundMusic.Main_Game_Waiting);
                break;
        }
    }

    public void ChangeBackground(E_BackGroundMusic background)
    {
        BackgroundMusic.Stop();
        Debug.Log("Test");
        switch (background)
        {
            case E_BackGroundMusic.Main_Menu_Background:
                Debug.Log("Test");
                BackgroundMusic = MainMenuBackground;
                BackgroundMusic.Play();
                break;
            case E_BackGroundMusic.Main_Game_Waiting:
                BackgroundMusic = MainGameWaiting;
                BackgroundMusic.Play();
                break;
            case E_BackGroundMusic.Main_Game_Background:
                BackgroundMusic = MainGameBackground;
                BackgroundMusic.Play();
                break;
        }
    }



    

    

}
