using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_UI_Mnager : MonoBehaviour
{
    [SerializeField] GameObject Play_Btn;
    [SerializeField] GameObject Exit_Btn;
    [SerializeField] AudioClip Btn_sfx;
    AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    public void Play()
    {
        SceneLoader.Load(SceneLoader.Scenes.Game);
        Sfx_Btn_s();
    }
    
    public void Exit()
    {
        Application.Quit();
        Sfx_Btn_s();
    }
    
    void Sfx_Btn_s()
    {
        audioSource.PlayOneShot(Btn_sfx);
    }
}
