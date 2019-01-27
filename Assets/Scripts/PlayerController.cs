using System.Collections;
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
    
    private Vector2 _direction;
    private GameObject[] planets;
    private Rigidbody2D rb;
    private Camera _cam;
    private float currentUpwardForce = 0;

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

        planets = GameObject.FindGameObjectsWithTag("Planet");
        _cam = GameObject.FindObjectOfType<Camera>();
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
        rb.AddForce(((-transform.right * currentUpwardForce) * jumpForce) * Time.deltaTime);
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
        Quaternion q = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), (Time.fixedDeltaTime * 2));
        transform.rotation = q;
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
        currentUpwardForce = 0;
    }

    public void Jump(){
        CheckPlayer();
        currentUpwardForce = 1;
    }

    private void CheckPlayer(){
         if (!photonView.IsMine){ return; }
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
}