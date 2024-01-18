using UnityEngine;
using FishNet.Object;
using FishNet.Example;

public class Online_Game_UI_Manager : NetworkBehaviour
{
    [SerializeField] private GameObject Pause_Btn;
    [SerializeField] private GameObject Pause_Menu;
    [SerializeField] private AudioClip Btn_sfx;
    private AudioSource audioSource;
    public Online_Phone_Camera_Controller cameraController;
    public NetworkHudCanvases networkHud;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        cameraController = GetComponentInParent<Online_Phone_Camera_Controller>();
        networkHud = new NetworkHudCanvases();

    }

    public void Pause()
    {
        Pause_Menu.SetActive(true);
        Pause_Btn.SetActive(false);
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
        Sfx_Btn_s();
        cameraController.Mobile_Camera.Stop();
        if(base.IsServer)
        {
            Debug.Log("Server");
            networkHud.OnClick_Server();
        }
        else
        {
            Debug.Log("Client");
            networkHud.OnClick_Client();
        }
        SceneLoader.Load(SceneLoader.Scenes.MainMenu);
    }

    public void Sfx_Btn_s()
    {
        audioSource.PlayOneShot(Btn_sfx);
    }
}
