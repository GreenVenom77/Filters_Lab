using UnityEngine;
using FishNet.Object;
using System.Collections.Generic;

public class Online_Connector : NetworkBehaviour
{
    [SerializeField] private GameObject _panel;
    public Online_Game_Manager _onlineGameManager;

    public override void OnStartClient()
    {
        base.OnStartClient();
        _onlineGameManager = FindObjectOfType<Online_Game_Manager>();

        if (base.IsOwner)
        {
            SendListRequest_Server();
        }
    }

    public override void OnStopClient()
    {
        base.OnStopClient();

        _onlineGameManager.players.Remove(gameObject);
        Debug.Log($"The Player {gameObject} has Left!!");

        if(base.IsOwner)
        {
            
        }
    }

    [ServerRpc]
    public void SendListRequest_Server()
    {
        Debug.Log("Recieved Request!!");
        SendListRequest();
    }

    [ObserversRpc]
    public void SendListRequest()
    {
        Debug.Log("Doing the Request!!");

        _onlineGameManager.players.Add(gameObject);
        Debug.Log($"The Player {gameObject} has Joined!!");
    }

    [ObserversRpc]
    public void FiltersUI_Request()
    {
        if(_panel.activeSelf == false)
        {
            _panel.SetActive(true);
            Debug.Log("UI has been activated");
        }
        else
        {
            _panel.SetActive(false);
            Debug.Log("UI has been Deactivated");
        }
    }
}
