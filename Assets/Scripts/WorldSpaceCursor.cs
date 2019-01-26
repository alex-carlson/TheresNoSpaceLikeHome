using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceCursor : MonoBehaviour
{
    public GameObject _activeObject;

    Vector3 _cursorPosition;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray;
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if(_activeObject)  _activeObject.SendMessage("Deselected", SendMessageOptions.DontRequireReceiver);
                _activeObject = hit.transform.gameObject;
                hit.transform.gameObject.SendMessage("Selected", SendMessageOptions.DontRequireReceiver);
            } else {
                if(!_activeObject) return;
                _activeObject.SendMessage("Deselected", SendMessageOptions.DontRequireReceiver);
                _activeObject = null;
            }

        }
    }
}
