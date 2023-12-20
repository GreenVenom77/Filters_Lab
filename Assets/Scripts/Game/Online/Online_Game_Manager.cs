using FishNet.Component.Spawning;
using FishNet.Connection;
using FishNet.Object;
using UnityEngine;

public class Online_Game_Manager : NetworkBehaviour
{
    [SerializeField] private PlayerSpawner _playerSpawner;
    [SerializeField] private AudioClip System_Clip;
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
    public void Remove_Nulls_Server()
    {
        Invoke("Remove_Nulls", 2f);
    }

    [ObserversRpc]
    public void Choose_Player()
    {
        print("start function");
        if (_playerSpawner.players.Count > 0)
        {
            print("start looking");
            if (_playerConnected)
            {
                _lastPlayer = _playerConnected;
            }
            
            int index = Random.Range(0, _playerSpawner.players.Count);
            print(_playerSpawner.players.Count);
            GameObject selectedPlayer = _playerSpawner.players[index].gameObject;
            _playerConnected = selectedPlayer.GetComponent<Online_Connector>();
            print(index);

            _lastPlayer.DisableFX_UI();
            _playerConnected.EnableFX_UI();
            _audioSource.PlayOneShot(System_Clip);
            Debug.Log(_playerConnected.gameObject);
        }
    }

    [ObserversRpc]
    public void Remove_Nulls()
    {
        _playerSpawner.players.RemoveAll(player => !player);
    }
}
