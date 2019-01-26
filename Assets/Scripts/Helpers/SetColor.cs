using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SetColor : MonoBehaviour{

    public MeshRenderer _renderer;
    public Color setColor;
    private Color _defaultColor;

    private void Awake()
    {
        _defaultColor = _renderer.material.color;
    }

    public void SetMaterialColor(){
        _renderer.material.color = setColor;
    }

    public void Reset(){
         _renderer.material.color = _defaultColor;
    }
}