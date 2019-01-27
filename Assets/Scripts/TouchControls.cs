using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControls : MonoBehaviour
{



    // Update is called once per frame
    void Update()
    {
        Touch myTouch = Input.GetTouch(0);

        Touch[] myTouches = Input.touches;

        if(myTouches.Length <= 0) return;
        
        for (int i = 0; i < Input.touchCount; i++)
        {
            //Do something with the touches
            Debug.Log(myTouches[i].position.x);
        }
    }
}
