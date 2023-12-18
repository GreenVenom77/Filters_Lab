using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using FishNet;
using FishNet.Managing;

public class Online_Game_UI_Manager : MonoBehaviour
{
    [SerializeField] GameObject Pause_Btn;
    [SerializeField] GameObject Pause_Menu;
    [SerializeField] AudioClip Btn_sfx;
    private AudioSource audioSource;
    private Online_Phone_Camera_Controller cameraController;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        cameraController = GetComponent<Online_Phone_Camera_Controller>();
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
        Sfx_Btn_s();
        cameraController.Mobile_Camera.Play();
    }

    public void Home()
    {
        //SceneLoader.Load(SceneLoader.Scenes.MainMenu);
        InstanceFinder.ServerManager.Despawn(gameObject);
        Sfx_Btn_s();
        Application.Quit();
    }

    public void Sfx_Btn_s()
    {
        audioSource.PlayOneShot(Btn_sfx);
    }
}
