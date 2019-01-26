using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10;
    public float moveSpeed = 5;
    
    private PhotonView photonView;
    private Camera _cam;
    private Vector2 _direction;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        _cam = GetComponentInChildren<Camera>();

        if(!photonView.IsMine && PhotonNetwork.IsConnected){
            _cam.enabled = false;
        }

        SetSpawnPosition();
    }

    private void SetSpawnPosition(){
        LevelGenerator lg = FindObjectOfType<LevelGenerator>();

        transform.position = lg.NextPlayerSpawnPoint.position;
        lg.MoveSpawn();
    }

    private void Update()
    {
        CheckPlayer();
        transform.Translate((_direction * moveSpeed) * Time.deltaTime, Space.Self);
    }

    public void MoveLeft(){
        CheckPlayer();
        _direction = -Vector2.right;
    }

    public void MoveRight(){
        CheckPlayer();
        _direction = Vector2.right;
    }

    public void ClearMovement(){
        CheckPlayer();
        _direction = Vector2.zero;
    }

    public void Jump(){
        CheckPlayer();
        GetComponent<Rigidbody2D>().AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void CheckPlayer(){
         if (photonView.IsMine == false && PhotonNetwork.IsConnected == true){  return; }
    }
}