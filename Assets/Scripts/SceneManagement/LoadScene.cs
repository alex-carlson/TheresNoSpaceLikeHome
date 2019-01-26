using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public Object scene;
    public void StartLoad(){
        SceneManager.LoadSceneAsync(scene.name, LoadSceneMode.Additive);
    }
}
