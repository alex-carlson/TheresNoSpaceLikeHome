using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class NetworkStatus : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        GetComponent<Text>().text = "connected players: " + PhotonNetwork.CountOfPlayers.ToString();
    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        UpdateText();
    }

    private void UpdateText(){
        GetComponent<Text>().text = "connected players: " + PhotonNetwork.CountOfPlayers.ToString();
    }
}
