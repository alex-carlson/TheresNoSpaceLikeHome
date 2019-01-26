using UnityEngine;
using UnityEngine.SceneManagement;

public class RemoveScenes : MonoBehaviour
{
    [SerializeField]
    public Object[] ScenesToUnload;

    void Start() {
        foreach(Object o in ScenesToUnload){
            if(SceneManager.GetSceneByName(o.name).isLoaded)
                SceneManager.UnloadSceneAsync(o.name);
        }
    }
}
