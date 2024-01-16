using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Phone_Camera_Controller : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject Ground;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Player_Vcam;
    [SerializeField] private GameObject World_Vcam;
    [SerializeField] private GameObject Turn_Right;
    [SerializeField] private GameObject Turn_Left;
    [SerializeField] private Game_UI_Manager manager;
    [SerializeField] private Player_Controller _playerController;
    public WebCamTexture Mobile_Camera;
    
    //Privates
    private WebCamDevice _camera;
    private Material Ground_Material;
    private Material Player_Material;
    private Vector3 _Rotation;
    private bool isDefault = true;
    private Player_Controller player_Controller;
    
    void Awake ()
    {
        Ground_Material = Ground.GetComponent<Renderer>().material;
        Player_Material = Player.GetComponent<Renderer>().material;
        Mobile_Camera = new WebCamTexture();
        _Rotation = Ground.transform.eulerAngles;
        player_Controller = GetComponent<Player_Controller>();
        Switch_Material_Object(isDefault);
    }

    private void Switch_Material_Object(bool isGround_Mat)
    {
        if (!isGround_Mat)
        {
            Ground_Material.mainTexture = null;
            Player_Material.mainTexture = Mobile_Camera;
        }
        else
        {
            Ground_Material.mainTexture = Mobile_Camera;
            Player_Material.mainTexture = null;
        }
    }

    public void Switch_Default()
    {
        isDefault = !isDefault;
        manager.Sfx_Btn_s();
        Switch_Material_Object(isDefault);
    }

    public void Switch_Vcam()
    {
        if (Player_Vcam.activeSelf)
        {
            Player_Vcam.SetActive(false);
            World_Vcam.SetActive(true);
            Turn_Right.SetActive(true);
            Turn_Left.SetActive(true);
            player_Controller.isPlayerCam = true;
        }
        else
        {
            Player_Vcam.SetActive(true);
            World_Vcam.SetActive(false);
            Turn_Right.SetActive(false);
            Turn_Left.SetActive(false);
            player_Controller.isPlayerCam = false;

        }
    }

    public void Right()
    {
        _Rotation.y -= 90;
        Ground.transform.rotation = Quaternion.Euler(0, _Rotation.y, 0);
        manager.Sfx_Btn_s();
    }

    public void Left()
    {
        _Rotation.y += 90;
        Ground.transform.rotation = Quaternion.Euler(0, _Rotation.y, 0);
        manager.Sfx_Btn_s();
    }

    public void Turn_Camera()
    {
        if (!Mobile_Camera.isPlaying)
        {
            Mobile_Camera.Play();
            isDefault = true;
            Switch_Material_Object(isDefault);
        }
        else
        {
            Mobile_Camera.Stop();
            Ground_Material.mainTexture = null;
            Player_Material.mainTexture = null;
        }
        manager.Sfx_Btn_s();
    }
}
