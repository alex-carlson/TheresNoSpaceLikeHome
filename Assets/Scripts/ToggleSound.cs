using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSound : MonoBehaviour
{

    public bool soundIsOn = true;
    public AudioSource music;
    public Sprite[] sprites;

    public void Toggle(){
        soundIsOn = !soundIsOn;

        if(soundIsOn){
            music.volume = 1;
            GetComponent<Image>().sprite = sprites[0];
            PlayerPrefs.SetInt("Mute", 0);
        } else {
            music.volume = 0;
            GetComponent<Image>().sprite = sprites[1];
            PlayerPrefs.SetInt("Mute", 1);
        }
    }
}
