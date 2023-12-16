using System.Collections;
using System.Collections.Generic;
using FishNet.Component.Spawning;
using FishNet.Connection;
using FishNet.Object;
using UnityEngine;

public class Online_Game_Manager : MonoBehaviour
{
    private PlayerSpawner _playerSpawner;
    private GameObject player;
    private Online_Connector _playerConnector;
    private int Index;
    private int Last_Index;

    void Start()
    {
        StartCoroutine(StartCoroutineOnObjectAdded());
    }

    private IEnumerator StartCoroutineOnObjectAdded()
    {
        yield return new WaitUntil(() => _playerSpawner.players.Count > 0);
        InvokeRepeating("Choose_Player", 0f, 10f);
    }

    public void Choose_Player()
    {
        if (player)
        {
            _playerConnector.EnableFX_UI();
        }

        Index = Random.Range(0, _playerSpawner.players.Count);

        if (Index == Last_Index)
        {
            Choose_Player();
        }
        else
        {
            Last_Index = Index;
            player = _playerSpawner.players[Index].gameObject;
            _playerConnector = player.GetComponent<Online_Connector>();
            _playerConnector.EnableFX_UI();
        }
    }
}
