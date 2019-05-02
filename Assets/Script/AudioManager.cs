using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum E_BackGroundMusic { Main_Menu_Background, Main_Game_Waiting, Main_Game_Background, Victory_Theme, Defeat_Theme}

public class AudioManager : MonoBehaviour
{
    [SerializeField] private static AudioManager Instance;

    [Header("Audio Source Reference")]
    public AudioSource MainMenuBackground;
    public AudioSource MainGameWaiting;
    public AudioSource MainGameBackground;
    public AudioSource VictoryTheme;
    public AudioSource DefeatTheme;

    [Header("Current Audio Playing")]
    public AudioSource BackgroundMusic;
    public AudioSource SoundEffect;

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
                Debug.Log(scene.name);
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
            case E_BackGroundMusic.Victory_Theme:
                BackgroundMusic = VictoryTheme;
                BackgroundMusic.Play();
                break;
            case E_BackGroundMusic.Defeat_Theme:
                BackgroundMusic = DefeatTheme;
                BackgroundMusic.Play();
                break;
        }
    }



    

    

}
