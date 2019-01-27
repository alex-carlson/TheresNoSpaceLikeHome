using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{

    public GameObject playerPrefab;
    public GameEvent Event;
    public Text playerCount;

    public bool sentSeed;
    public int seed;
    public LevelGenerator LevelGenerator;

    [HideInInspector]
    public PlayerController LocalPlayer;

    private void Start()
    {

        if(!PhotonNetwork.IsConnected){
            SceneManager.LoadScene("Menu");
            return;
        }

        if (playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'",this);
        }
        else
        {

            if(PlayerController.LocalPlayerInstance==null){
                Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManager.GetActiveScene().name);
                // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                PhotonNetwork.Instantiate(this.playerPrefab.name, LevelGenerator.NextPlayerSpawnPoint.position, Quaternion.identity, 0);
            } else {
                Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
            }
        }

        //PlayerController.RefreshInstance(ref LocalPlayer, playerPrefab);
    }

    private void Update()
    {
        if(PhotonNetwork.InRoom && PhotonNetwork.IsMasterClient && !sentSeed){
            sentSeed = true;
            seed = Mathf.RoundToInt(Random.value * 100000);
            LevelGenerator.realSeed = seed;
            PhotonView pv = LevelGenerator.GetComponent<PhotonView>();
            pv.RPC("RecieveSeed", RpcTarget.AllBuffered, seed);
            playerCount.text = "Players: " + PhotonNetwork.CurrentRoom.PlayerCount;
        }
    }

    // void LoadArena()
    // {
    // if (!PhotonNetwork.IsMasterClient)
    //     {
    //         Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
    //     }
    //     Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
    //     PhotonNetwork.LoadLevel("Game");
    // }

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting
        base.OnPlayerEnteredRoom(other);
        Event.Raise();

        //PlayerController.RefreshInstance(ref LocalPlayer, playerPrefab);

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
            //LoadArena();
        }

        playerCount.text = "Players: " + PhotonNetwork.CurrentRoom.PlayerCount;
    }


    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects


        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
        }
        playerCount.text = "Players: " + PhotonNetwork.CurrentRoom.PlayerCount;
    }

    public override void OnLeftRoom(){
        SceneManager.LoadScene("Menu");
    }
}
