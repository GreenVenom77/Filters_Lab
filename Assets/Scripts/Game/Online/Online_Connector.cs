using System.Collections;
using System.Collections.Generic;
using FishNet.Component.Spawning;
using FishNet.Object;
using UnityEngine;

public class Online_Connector : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    public void EnableFX_UI()
    {
        if (_panel.activeSelf)
        {
            _panel.SetActive(false);
        }
        else
        {
            _panel.SetActive(true);
        }
    }
}
