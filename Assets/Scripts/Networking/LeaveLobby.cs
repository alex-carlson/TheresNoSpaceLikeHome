using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LeaveLobby : MonoBehaviour
{
    public void LeaveRoom(){
        PhotonNetwork.LeaveRoom();
    }
}
