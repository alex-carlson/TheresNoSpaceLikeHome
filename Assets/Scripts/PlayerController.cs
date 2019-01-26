using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10;
    
    private PhotonView photonView;
    private Camera _cam;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        _cam = GetComponentInChildren<Camera>();
    }

    private void Awake()
    {
        if(!photonView.IsMine && PhotonNetwork.IsConnected){
            _cam.enabled = false;
        }
    }

   private  void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true){  return; }

        if(Input.GetKeyDown(KeyCode.Space)){
            Jump(jumpForce);
        }
    }

    private void LateUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        Vector3 mov = transform.right * x;

        Move(mov);
    }

    public void Move(Vector3 movement){
        transform.Translate(movement);
    }

    public void Jump(float force){
        GetComponent<Rigidbody2D>().AddForce(transform.up * force, ForceMode2D.Impulse);
    }
}
