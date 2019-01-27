using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Vector3 Offset;
    public Transform Target;

    private void Update()
    {
        if(Target == null) return;

        transform.position = Target.position + Offset;
    }

    public void SetTarget(Transform t){
        Target = t;
    }
}
