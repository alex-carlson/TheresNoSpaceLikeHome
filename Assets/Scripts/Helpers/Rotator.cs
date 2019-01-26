using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    public Vector3 Direction;
    [Range(0, 500)] public float speed = 1;

    void Update()
    {
        transform.Rotate(Direction * speed * Time.deltaTime);
    }
}
