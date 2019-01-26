using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{

    private Color _defaultColor;

    private void Start()
    {
        _defaultColor = GetComponent<MeshRenderer>().material.color;
    }

    public void Selected(){
        GetComponent<MeshRenderer>().material.color = Color.yellow;
    }

    public void Deselected(){
        GetComponent<MeshRenderer>().material.color = _defaultColor;
    }
}
