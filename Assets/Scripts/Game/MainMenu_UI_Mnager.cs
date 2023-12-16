using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_UI_Mnager : MonoBehaviour
{
    [SerializeField] GameObject Main_Panel;
    [SerializeField] GameObject Game_Panel;
    [SerializeField] GameObject Settings_Panel;
    [SerializeField] AudioClip Btn_sfx;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    public void SinglePlayer()
    {
        SceneLoader.Load(SceneLoader.Scenes.Game);
        Sfx_Btn_s();
    }

    public void MultiPlayer()
    {
        SceneLoader.Load(SceneLoader.Scenes.Online_Game);
        Sfx_Btn_s();
    }

    public void Game()
    {
        Main_Panel.SetActive(false);
        Game_Panel.SetActive(true);
        Sfx_Btn_s();
    }

    public void Back()
    {
        Main_Panel.SetActive(true);
        Game_Panel.SetActive(false);
        Settings_Panel.SetActive(false);
        Sfx_Btn_s();
    }

    public void Settings()
    {
        Main_Panel.SetActive(false);
        Settings_Panel.SetActive(true);
        Sfx_Btn_s();
    }
    
    public void Exit()
    {
        Sfx_Btn_s();
        Application.Quit();
    }
    
    void Sfx_Btn_s()
    {
        audioSource.PlayOneShot(Btn_sfx);
    }
}
