using UnityEngine;
using FishNet.Connection;
using FishNet.Object;

public class Online_Phone_Camera_Controller : NetworkBehaviour
{
    public GameObject Player_Body;
    public WebCamTexture Mobile_Camera;
    public WebCamDevice[] Devices;
    private Material Body_Material;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsNetworked)
        {
            Devices = WebCamTexture.devices;
            Collecting_Cameras();
        }
        else
        {
            
        }
    }

    public override void OnStopClient()
    {
        base.OnStopClient();
        if(base.IsOwner)
        {
            Mobile_Camera.Stop();
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
        Body_Material = Player_Body.GetComponent<Renderer>().material;

        foreach (WebCamDevice camera in Devices)
        {
            if(camera.isFrontFacing)
            {
                Mobile_Camera = new WebCamTexture(camera.name, 640, 480);
                Mobile_Camera.Play();
                Body_Material.mainTexture = Mobile_Camera;
            }
        }
    }
}
