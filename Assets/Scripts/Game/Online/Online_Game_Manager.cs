using System.Collections.Generic;
using FishNet.Connection;
using FishNet.Object;
using UnityEngine;

public class Online_Game_Manager : NetworkBehaviour
{
    [SerializeField] private float firstPlayerTimer = 4f;
    [SerializeField] private float nextPlayerTimer = 8f;
    [SerializeField] private AudioClip System_Clip;
    public List<GameObject> players;
    private Online_Connector _playerConnected;
    private Online_Connector _lastPlayer;
    private AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        InvokeRepeating(nameof(Choose_Player), firstPlayerTimer, nextPlayerTimer);
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
                _lastPlayer.FiltersUI_Request();
            }
            
            int index = Random.Range(0, players.Count);
            print(players.Count);
            GameObject selectedPlayer = players[index].gameObject;
            _playerConnected = selectedPlayer.GetComponent<Online_Connector>();
            print(index);

            _playerConnected.FiltersUI_Request();
            _audioSource.PlayOneShot(System_Clip);
            Debug.Log(_playerConnected.gameObject);
        }
    }
}
