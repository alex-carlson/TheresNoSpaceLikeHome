using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSound : MonoBehaviour
{

    public bool soundIsOn = true;
    public AudioSource music;

    public void Toggle(){
        soundIsOn = !soundIsOn;
        if(soundIsOn){
            music.volume = 1;
        } else {
            music.volume = 0;
        }
    }
}
