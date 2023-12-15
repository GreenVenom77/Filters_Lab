using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;

public class Online_Phone_Camera_Controller : NetworkBehaviour
{
    private WebCamTexture Mobile_Camera;
    private WebCamDevice[] Devices;
    private WebCamDevice Camera;

    void Awake()
    {
        Collecting_cameras();
    }

    private void Collecting_cameras()
    {
        Devices = WebCamTexture.devices;
        foreach (WebCamDevice camera in Devices)
        {
            if (camera.isFrontFacing)
            {
                Mobile_Camera = new WebCamTexture(camera.name);
                break;
            }
        }
        GetComponent<Renderer>().material.mainTexture = Mobile_Camera;
        Mobile_Camera.Play();
    }
}
