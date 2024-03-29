﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public Vector3 direction;
    [Range(0, 50)] public float speedScale;
    [Range(0, 10f)] public float distanceScale;
    float randomOffset;

    private void Start()
    {
        randomOffset = Random.value;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = direction * (Mathf.Cos((Time.time + randomOffset) * speedScale) );

        transform.position = transform.position + (v * ((distanceScale) * Time.deltaTime));
    }
}
