using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;

public class Online_Phone_Camera_Controller : NetworkBehaviour
{
    private GameObject Player_Body;
    public WebCamTexture Mobile_Camera;
    private WebCamDevice[] Devices;
    private WebCamDevice Camera;
    private Material Body_Material;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            Player_Body = GameObject.Find("GFX");
            Body_Material = Player_Body.GetComponent<Renderer>().material;
            Collecting_Cameras_Server();
        }
        else
        {

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
        Body_Material.mainTexture = cameraTexture;
    }
}
