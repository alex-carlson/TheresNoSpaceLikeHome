using UnityEngine;
using UnityEngine.SceneManagement;

public class AddScenes : MonoBehaviour
{
    [SerializeField]
    public Object[] RequiredScenes;

    void Start() {
        foreach(Object o in RequiredScenes){
            if(!SceneManager.GetSceneByName(o.name).isLoaded)
                SceneManager.LoadSceneAsync(o.name, LoadSceneMode.Additive);
        }
    }
}
