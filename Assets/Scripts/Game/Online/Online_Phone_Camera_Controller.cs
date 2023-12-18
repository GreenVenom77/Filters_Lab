using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;

public class Online_Phone_Camera_Controller : NetworkBehaviour
{
    [SerializeField] private GameObject Player_Body;
    private WebCamTexture Mobile_Camera;
    private WebCamDevice[] Devices;
    private WebCamDevice Camera;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            Collecting_Cameras_Server();
        }
        else
        {
            GetComponent<Online_Phone_Camera_Controller>().enabled = false;
        }
    }

    [ServerRpc]
    public void Collecting_Cameras_Server()
    {
        Collecting_Cameras();
    }

    [ObserversRpc]
    public void Collecting_Cameras()
    {
        Devices = WebCamTexture.devices;
        foreach (WebCamDevice camera in Devices)
        {
            if (camera.isFrontFacing)
            {
                Mobile_Camera = new WebCamTexture(camera.name);
                Mobile_Camera.Play();
                CameraTextureOnClients(Mobile_Camera);
            }
        }
    }

    public void CameraTextureOnClients(WebCamTexture cameraTexture)
    {
        // This method is called on all clients, so set the texture on the local player's body
        Player_Body.GetComponent<Renderer>().material.mainTexture = cameraTexture;
    }
}
