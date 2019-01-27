﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPun
{
    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;
    public float jumpForce = 10;
    public float moveSpeed = 5;
    public bool isGrounded = false;
    public Transform pullTarget = null;
    public float maxGravDist = 4f;
    public float maxGravity = 35f;
    
    private Camera _cam;
    private Vector2 _direction;
    private GameObject[] planets;
    private Rigidbody2D rb;

    private void Start()
    {

        if(photonView.IsMine && PhotonNetwork.IsConnected){
            FindObjectOfType<Camera>().GetComponent<FollowPlayer>().SetTarget(this.transform);
        }

        if (photonView.IsMine)
        {
            PlayerController.LocalPlayerInstance = this.gameObject;
        } else {
            this.enabled = false;
        }
        // #Critical
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
        DontDestroyOnLoad(this.gameObject);

        planets = GameObject.FindGameObjectsWithTag("Planet");
        rb = GetComponent<Rigidbody2D>();

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
        pullTarget = GetClosestPlanet();
    }

    private void FixedUpdate()
    {
        if(!pullTarget) return;
        
        float dist = Vector3.Distance(pullTarget.position, transform.position);
        if (dist <= maxGravDist)
        {
            Vector3 v = pullTarget.position - transform.position;
            rb.AddForce(v.normalized * (1.0f - dist / maxGravDist) * maxGravity);
        }

        var dir = pullTarget.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void MoveLeft(){
        CheckPlayer();
        _direction = -Vector2.up;
    }

    public void MoveRight(){
        CheckPlayer();
        _direction = Vector2.up;
    }

    public void ClearMovement(){
        CheckPlayer();
        _direction = Vector2.zero;
    }

    public void Jump(){
        CheckPlayer();
        GetComponent<Rigidbody2D>().AddForce(-transform.right * jumpForce, ForceMode2D.Impulse);
    }

    private void CheckPlayer(){
         if (photonView.IsMine == false && PhotonNetwork.IsConnected == true){  return; }
    }

    private Transform GetClosestPlanet(){

        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;

        foreach (GameObject g in planets){
            float dist = Vector3.Distance(g.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = g.transform;
                minDist = dist;
            }
        }

        return tMin;
    }

    public static void RefreshInstance(ref PlayerController player, GameObject prefab){
        var position = Vector3.zero;
        var rotation = Quaternion.identity;
        if(player != null){
            position = player.transform.position;
            rotation = player.transform.rotation;
            PhotonNetwork.Destroy(player.gameObject);
        }

        player = PhotonNetwork.Instantiate(prefab.gameObject.name, position, rotation).GetComponent<PlayerController>();
    }
}