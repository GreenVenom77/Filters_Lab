using UnityEngine;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;

public class Online_Phone_Camera_Controller : NetworkBehaviour
{
    public GameObject Player_Body;
    public WebCamTexture Mobile_Camera;
    private WebCamDevice[] Devices;
    private WebCamDevice Camera;
    private Material Body_Material;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            Body_Material = Player_Body.GetComponent<Renderer>().material;
            Devices = WebCamTexture.devices;
            Invoke("Collecting_Cameras_Server", 0.5f);
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
        foreach (WebCamDevice camera in Devices)
        {
            if (camera.isFrontFacing)
            {
                Mobile_Camera = new WebCamTexture(camera.name);
                Body_Material.mainTexture = Mobile_Camera;
                Mobile_Camera.Play();
            }
        }
    }
}
