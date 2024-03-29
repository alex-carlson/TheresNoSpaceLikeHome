﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPun
{
    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;
    public float jumpForce = 50;
    public float moveSpeed = 15;
    public bool isGrounded = false;
    public Transform pullTarget = null;
    public float maxGravDist = 100f;
    public float maxGravity = 80f;
    public float planetDiameter = 10;
    public float JumpAnimTimeOffset = 0.5f;
    public AudioClip[] footsteps;
    public AudioClip jumpSound;
    public int characterIndex = 0;
    public RuntimeAnimatorController[] characterAnimators;
    
    private Vector2 _direction;
    private GameObject[] planets;
    private Rigidbody2D rb;
    private Camera _cam;
    private float currentUpwardForce = 0;
    private bool active = false;
    private Animator anim;
    private SpriteRenderer sprite;
    private AudioSource audio;
    private ParticleSystem particles;
    private int muted;

    private void Start()
    {

        if(photonView.IsMine){
            FindObjectOfType<Camera>().GetComponent<FollowPlayer>().SetTarget(this.transform);
            PlayerController.LocalPlayerInstance = this.gameObject;
        } else {
            this.enabled = false;
        }
        // #Critical
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
        DontDestroyOnLoad(this.gameObject);

        _cam = GameObject.FindObjectOfType<Camera>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
        particles = GetComponentInChildren<ParticleSystem>();
        muted = PlayerPrefs.GetInt("Mute");

        SetSpawnPosition();

        Invoke("Init", 1);
    }

    private void SetSpawnPosition(){
        LevelGenerator lg = FindObjectOfType<LevelGenerator>();
        transform.position = lg.NextPlayerSpawnPoint.position;
        lg.MoveSpawn();
    }

    public void Init(){
        active = true;
        planets = GameObject.FindGameObjectsWithTag("Planet");
    }

    private void Update()
    {
        if(!active) return;
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
        Quaternion q = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle + 90, Vector3.forward), (Time.fixedDeltaTime * 5f));
        transform.rotation = q;
    }

    public void MoveLeft(){
        if(!photonView.IsMine) return;
        _direction = -Vector2.right;
        anim.SetFloat("moveSpeed", 1);
        sprite.flipX = false;
        if(muted == 0)
            InvokeRepeating("Step", 0, 0.25f);
    }

    public void MoveRight(){
        if (!photonView.IsMine) return;
        _direction = Vector2.right;
        anim.SetFloat("moveSpeed", 1);
        sprite.flipX = true;
        particles.emissionRate = 5;
        if (muted == 0)
            InvokeRepeating("Step", 0, 0.25f);
    }

    public void ClearMovement(){
        if (!photonView.IsMine) return;
        _direction = Vector2.zero;
        currentUpwardForce = 0;
        anim.SetFloat("moveSpeed", 0);
        anim.SetBool("jumping", false);
        particles.emissionRate = 0;
        CancelInvoke("Step");
    }

    public void Jump(){
        if (!photonView.IsMine) return;
        float dist = Vector3.Distance(this.transform.position, this.pullTarget.position);

        if(dist < planetDiameter)
        {
            anim.SetBool("jumping", true);
        }

        Invoke("PushOff", JumpAnimTimeOffset);
    }

    private void PushOff(){
        particles.emissionRate = 50;
        anim.SetBool("jumping", false);

        float dist = Vector3.Distance(transform.position, pullTarget.position);

        if (dist < planetDiameter){
            rb.AddForce((transform.up * jumpForce), ForceMode2D.Impulse);
            if(muted == 0)
                audio.PlayOneShot(jumpSound);
        }
    }

    private void CheckPlayer(){
         if (!photonView.IsMine){ return; }
    }

    void Step(){
        int r = Mathf.RoundToInt(footsteps.Length - 1);
        audio.PlayOneShot(footsteps[r]);
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

    public void SetCharacter(int index){
        characterIndex = index;
        if(characterIndex > characterAnimators.Length) characterIndex = 0;
        anim.runtimeAnimatorController = characterAnimators[characterIndex];
    }
}