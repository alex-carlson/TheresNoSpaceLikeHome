using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerConnector : MonoBehaviour
{

    public void MoveLeft(){
        PlayerController[] players = FindObjectsOfType<PlayerController>();

        foreach (PlayerController p in players)
        {
            p.MoveLeft();
        }
    }

    public void MoveRight(){
        PlayerController[] players = FindObjectsOfType<PlayerController>();

        foreach (PlayerController p in players)
        {
            p.MoveRight();
        }
    }

    public void Jump(){
        PlayerController[] players = FindObjectsOfType<PlayerController>();

        foreach (PlayerController p in players)
        {
            p.Jump();
        }
    }

    public void Clear(){
        PlayerController[] players = FindObjectsOfType<PlayerController>();

        foreach (PlayerController p in players)
        {
            p.ClearMovement();
        }
    }
}
