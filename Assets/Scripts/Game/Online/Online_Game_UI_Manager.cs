using UnityEngine;
using FishNet.Object;

public class Online_Game_UI_Manager : NetworkBehaviour
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
        LeaveServerServerRpc();
        Sfx_Btn_s();
        SceneLoader.Load(SceneLoader.Scenes.MainMenu);
    }

    public void Sfx_Btn_s()
    {
        audioSource.PlayOneShot(Btn_sfx);
    }

    [ServerRpc]
    private void LeaveServerServerRpc()
    {
        LeaveServerClientRpc();
    }

    [ObserversRpc]
    private void LeaveServerClientRpc()
    {
            Destroy(gameObject);
    }
}
