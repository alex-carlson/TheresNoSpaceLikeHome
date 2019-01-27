using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Networking : MonoBehaviourPunCallbacks
{
    public string gameVersion = "1";
    public Text progressLabel;

    private bool isConnecting;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void Connect(){
        isConnecting = true;
        SetLabel("Connecting...");
        if(PhotonNetwork.IsConnected){
            PhotonNetwork.JoinRandomRoom();
        } else {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster(){
        SetLabel("Connected to Server");
        base.OnConnectedToMaster();
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");
        SetLabel("No room available, creating one.");
        // progressLabel.text = "Failed to join existing room, starting a new game";
        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(null, new RoomOptions());
    }

    public override void OnDisconnected(DisconnectCause cause){
        base.OnDisconnected(cause);
    }

    public override void OnJoinedRoom(){
        base.OnJoinedRoom();
        SetLabel("Connected!");
        PhotonNetwork.LoadLevel("Game");
    }

    private void SetLabel(string s)
    {
        progressLabel.text = s;
    }
}
