using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10;

    // Update is called once per frame
    void Update()
    {
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
