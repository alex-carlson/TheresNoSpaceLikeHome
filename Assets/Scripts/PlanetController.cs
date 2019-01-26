using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D col){
        Destroy(col.gameObject);
    }
}
