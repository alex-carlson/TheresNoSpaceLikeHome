using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public float Size = 10;
    public float PlanetDistance = 1;
    public List<GameObject> PlanetSprites;
    public GameObject PlayerSpawnGO;
    public float playerCount = 4;

    private void Start(){
        // create a starting point for each player that we have
        SpawnMap();
    }

    private void SpawnMap(){
        for(int i = 0; i < Size; i ++){
            for(int j = 0; j < Size; j++){
                 int r = Mathf.FloorToInt(Random.value * PlanetSprites.Count);
                 Vector2 pos = new Vector2(i * PlanetDistance, j * PlanetDistance);
                 GameObject planet = Instantiate(PlanetSprites[r], pos + RandomVector(3f), Quaternion.Euler(Vector3.zero));
                 SetRandomColor(planet);
                 planet.transform.SetParent(this.transform);
            }
        }
    }

    private void SpawnPoint(int playerIndex){
        // create starting planet
        Vector2 StartPoint = RandomVector(Size);

        Instantiate(PlayerSpawnGO,StartPoint, Quaternion.Euler(Vector3.zero));
    }

    private Vector2 RandomVector(float scale){
        Vector2 v =  new Vector2(Random.value * scale, Random.value * scale);

        return v;
    }

    private void SetRandomColor(GameObject go){
        go.GetComponent<SpriteRenderer>().color =  Random.ColorHSV(0f, 1f, 0.6f, 0.8f, 0.5f, 0.8f);
    }
}
