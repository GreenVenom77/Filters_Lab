using System.Collections.Generic;
using FishNet.Connection;
using FishNet.Object;
using UnityEngine;

public class Online_Game_Manager : NetworkBehaviour
{
    [SerializeField] private AudioClip System_Clip;
    public List<GameObject> players;
    private Online_Connector _playerConnected;
    private Online_Connector _lastPlayer;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Choose_Player", 3f, 8f);
    }

    [ServerRpc]
    public void Choose_Player_Server()
    {
        Choose_Player();
    }

    [ServerRpc]
    public void SendListRequest_Server(GameObject player)
    {
        Debug.Log("Recieved Request!!");
        SendListRequest(player);
    }

    [ObserversRpc]
    public void Choose_Player()
    {
        print("start function");
        if (players.Count > 0)
        {
            print("start looking");
            if (_playerConnected)
            {
                _lastPlayer = _playerConnected;
                _lastPlayer.DisableFX_UI();
            }
            
            int index = Random.Range(0, players.Count);
            print(players.Count);
            GameObject selectedPlayer = players[index].gameObject;
            _playerConnected = selectedPlayer.GetComponent<Online_Connector>();
            print(index);

            _playerConnected.EnableFX_UI();
            _audioSource.PlayOneShot(System_Clip);
            Debug.Log(_playerConnected.gameObject);
        }
    }

    [ObserversRpc]
    public void SendListRequest(GameObject player)
    {
        Debug.Log("Doing the Request!!");
        players.Add(player);
        Debug.Log("Joined!!");
    }
}
