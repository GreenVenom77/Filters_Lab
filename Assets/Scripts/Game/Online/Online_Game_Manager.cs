using FishNet.Component.Spawning;
using FishNet.Connection;
using FishNet.Object;
using UnityEngine;

public class Online_Game_Manager : NetworkBehaviour
{
    [SerializeField] private PlayerSpawner _playerSpawner;
    [SerializeField] private AudioClip System_Clip;
    private Online_Connector _playerConnector;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Choose_Player_Server", 0f, 8f);
    }

    [ServerRpc]
    public void Choose_Player_Server()
    {
        if (_playerSpawner.players.Count > 0)
        {
            // Disable UI for the previous player
            if (_playerConnector)
            {
                _playerConnector.EnableFX_UI();
                Debug.Log("PlayerConnector");
            }

            // Choose a random player from the list
            int index = Random.Range(0, _playerSpawner.players.Count);
            GameObject selectedPlayer = _playerSpawner.players[index].gameObject;
            _playerConnector = selectedPlayer.GetComponent<Online_Connector>();

            // Play audio and enable UI for the selected player
            _audioSource.PlayOneShot(System_Clip);
            _playerConnector.EnableFX_UI();
        }
    }
}
