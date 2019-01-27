using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Pun;
using UnityEngine.UI;

public class LevelGenerator : MonoBehaviour
{
    public float Size = 10;
    public float PlanetDistance = 1;
    public float displacement = 5;
    public List<GameObject> SpecialPlanets;
    public List<GameObject> NormalPlanets;
    public Transform NextPlayerSpawnPoint;
    public int realSeed = 0;
    public GameManager GameManager;
    public Text seedText;

    private List<Planet> Planets;

    private void SpawnMap(){
        Planets = new List<Planet>();
        
        for(int i = 0; i < Size; i ++){
            for(int j = 0; j < Size; j++){
                 int r = Mathf.FloorToInt(Random.value * NormalPlanets.Count);
                 Vector2 pos = new Vector2(i * PlanetDistance, j * PlanetDistance);
                 Planet p = new Planet();
                 p.x = pos.x;
                 p.y = pos.y;
                 p.spriteIndex = r;
                 Planets.Add(p);
                 GameObject planet = Instantiate(NormalPlanets[r], pos + RandomVector(displacement), Quaternion.Euler(Vector3.zero));
                 planet.transform.SetParent(this.transform);
            }
        }

        for(int i = 0; i < SpecialPlanets.Count; i++){
            Vector2 r = RandomVector(Size * 2);

           GameObject planet = Instantiate(SpecialPlanets[i], r, Quaternion.identity);
           planet.name = "Special Planet";
           planet.transform.SetParent(this.transform);
        }
    }

    public void MoveSpawn(){
        NextPlayerSpawnPoint.position = RandomVector(Size * 2);
    }

    private Vector2 RandomVector(float scale){
        Vector2 v =  new Vector2(Random.value * scale, Random.value * scale);

        return v;
    }

    [PunRPC]
    void RecieveSeed(int s){
        realSeed = s;
        seedText.text = "seed: " + realSeed;
        Random.InitState(realSeed);
        SpawnMap();
    }
}

[System.Serializable]
public class Planet {
    public float x;
    public float y;
    public int spriteIndex;
}