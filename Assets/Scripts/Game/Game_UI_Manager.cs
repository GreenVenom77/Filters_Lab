using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_UI_Manager : MonoBehaviour
{
    [SerializeField] GameObject Pause_Btn;
    [SerializeField] GameObject Pause_Menu;
    [SerializeField] GameObject Resume_Btn;
    [SerializeField] GameObject Home_Btn;
    [SerializeField] AudioClip Btn_sfx;
    private AudioSource audioSource;
    private Phone_Camera_Controller cameraController;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        cameraController = GetComponent<Phone_Camera_Controller>();
    }

    public void Pause()
    {
        Pause_Menu.SetActive(true);
        Pause_Btn.SetActive(false);
        Time.timeScale = 0f;
        Sfx_Btn_s();
        cameraController.Mobile_Camera.Stop();
    }
    
    public void Resume()
    {
        Pause_Menu.SetActive(false);
        Pause_Btn.SetActive(true);
        Time.timeScale = 1f;
        Sfx_Btn_s();
        cameraController.Mobile_Camera.Play();
    }

    public void Home()
    {
        SceneLoader.Load(SceneLoader.Scenes.MainMenu);
        Time.timeScale = 1f;
        Sfx_Btn_s();
    }

    public void Sfx_Btn_s()
    {
        audioSource.PlayOneShot(Btn_sfx);
    }
}
