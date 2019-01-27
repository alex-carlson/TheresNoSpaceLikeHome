using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public float Size = 10;
    public float PlanetDistance = 1;
    public float displacement = 5;
    public List<GameObject> PlanetSprites;
    public Transform NextPlayerSpawnPoint;

    private List<Planet> Planets;

    private void Start(){
        // create a starting point for each player that we have
        SpawnMap();
    }

    private void SpawnMap(){
        Planets = new List<Planet>();
        
        for(int i = 0; i < Size; i ++){
            for(int j = 0; j < Size; j++){
                 int r = Mathf.FloorToInt(Random.value * PlanetSprites.Count);
                 Vector2 pos = new Vector2(i * PlanetDistance, j * PlanetDistance);
                 Planet p = new Planet();
                 p.x = pos.x;
                 p.y = pos.y;
                 p.spriteIndex = r;
                 Planets.Add(p);
                 GameObject planet = Instantiate(PlanetSprites[r], pos + RandomVector(displacement), Quaternion.Euler(Vector3.zero));
                 planet.transform.SetParent(this.transform);
            }
        }
    }

    public void MoveSpawn(){
        NextPlayerSpawnPoint.position = RandomVector(Size * 2);
    }

    private Vector2 RandomVector(float scale){
        Vector2 v =  new Vector2(Random.value * scale, Random.value * scale);

        return v;
    }
}

[System.Serializable]
public class Planet {
    public float x;
    public float y;
    public int spriteIndex;
}