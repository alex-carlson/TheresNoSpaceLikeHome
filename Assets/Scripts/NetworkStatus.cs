using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class NetworkStatus : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Text>().text = "connected players: " + PhotonNetwork.CountOfPlayers.ToString();
    }
}
