using System.Collections.Generic;
using System.Collections;
using FishNet.Connection;
using FishNet.Object;
using UnityEngine;

public class Online_Game_Manager : NetworkBehaviour
{
    [SerializeField] private float firstPlayerTimer = 4f;
    [SerializeField] private float nextPlayerTimer = 8f;
    [SerializeField] private AudioClip System_Clip;
    public List<GameObject> players;
    private Online_Connector _newConnection;
    private Online_Connector _lastConnection;
    private AudioSource _audioSource;

    public override void OnStartServer()
    {
        base.OnStartServer();

        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(ChoosePlayerCoroutine(players, _newConnection, _lastConnection));
    }

    [ServerRpc(RequireOwnership = false)]
    public void Choose_Player_Server(List<GameObject> players, Online_Connector _newPlayer, Online_Connector _lastPlayer)
    {
        Choose_Player(players, _newPlayer, _lastPlayer);
    }

    [ObserversRpc(BufferLast = true)]
    public void Choose_Player(List<GameObject> players, Online_Connector _newPlayer, Online_Connector _lastPlayer)
    {
        print("start function");
        if (players.Count > 0)
        {
            print("start looking");
            if (_newPlayer)
            {
                _lastPlayer = _newPlayer;
                _lastPlayer.FiltersUI_Request();
            }
            
            int index = Random.Range(0, players.Count);
            print(players.Count);
            GameObject selectedPlayer = players[index].gameObject;
            _newPlayer = selectedPlayer.GetComponent<Online_Connector>();
            print(index);

            _newPlayer.FiltersUI_Request();
            _audioSource.PlayOneShot(System_Clip);
            Debug.Log(_newPlayer.gameObject);
        }
    }

    IEnumerator ChoosePlayerCoroutine(List<GameObject> players, Online_Connector _newPlayer, Online_Connector _lastPlayer)
    {
        yield return new WaitForSeconds(firstPlayerTimer);

        while (true)
        {
            Choose_Player_Server(players, _newPlayer, _lastPlayer);

            yield return new WaitForSeconds(nextPlayerTimer);
        }
    }
}
