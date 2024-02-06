using System.Collections.Generic;
using System.Collections;
using FishNet.Connection;
using FishNet.Broadcast;
using FishNet;
using UnityEngine;

public class Online_Game_Manager : MonoBehaviour
{
    [SerializeField] private float firstPlayerTimer = 4f;
    [SerializeField] private float nextPlayerTimer = 8f;
    [SerializeField] private AudioClip System_Clip;
    public List<GameObject> players;
    private AudioSource _audioSource;


    void OnEnable()
    {
        InstanceFinder.ClientManager.RegisterBroadcast<ChosenPlayers>(OnPlayersBroadcast);
        _audioSource = GetComponent<AudioSource>();
        print("Enabled!");

    }

    void OnDisable()
    {
        InstanceFinder.ClientManager.UnregisterBroadcast<ChosenPlayers>(OnPlayersBroadcast);
    }


    private void OnPlayersBroadcast(ChosenPlayers chosenPlayers)
    {
        print("Got the players!");
        StartCoroutine(ChoosePlayerCoroutine(players, chosenPlayers));
    }


    public void Choose_Player(List<GameObject> players, ChosenPlayers chosenPlayers)
    {
        print("Is he server?");

        if(InstanceFinder.IsServer)
        {
            print("start function");
            if (players.Count > 0)
            {
                print("start looking");
                if (chosenPlayers._newPlayer)
                {
                    chosenPlayers._lastPlayer = chosenPlayers._newPlayer;
                    chosenPlayers._lastPlayer.FiltersUI_Request();
                }
                
                int index = Random.Range(0, players.Count);
                print(players.Count);
                GameObject selectedPlayer = players[index].gameObject;
                chosenPlayers._newPlayer = selectedPlayer.GetComponent<Online_Connector>();
                print(index);

                InstanceFinder.ServerManager.Broadcast(new ChosenPlayers { 
                    _newPlayer = chosenPlayers._newPlayer,
                    _lastPlayer = chosenPlayers._lastPlayer
                });
                
                chosenPlayers._newPlayer.FiltersUI_Request();
                Debug.Log(chosenPlayers._newPlayer.gameObject);
            }
        }

        _audioSource.PlayOneShot(System_Clip);
    }

    IEnumerator ChoosePlayerCoroutine(List<GameObject> players, ChosenPlayers chosenPlayers)
    {
        print("Started counting!");
        yield return new WaitForSeconds(firstPlayerTimer);

        while (true)
        {
            Choose_Player(players, chosenPlayers);

            yield return new WaitForSeconds(nextPlayerTimer);
        }
    }


    public struct ChosenPlayers : IBroadcast
    {
        public Online_Connector _newPlayer;
        public Online_Connector _lastPlayer;
    }
}
