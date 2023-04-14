using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static int asteroidCount = 0;

    [SerializeField]
    int MaxAsteroids;

    [SerializeField]
    GameObject Asteroid;

    [SerializeField]
    float MinimumInterval, MaximumInterval;

    Ship ship;

    [SerializeField]
    GameManager manager;

    float NextSpawn;
    // Start is called before the first frame update
    void Start()
    {
        ship = GameObject.FindFirstObjectByType<Ship>();
        NextSpawn = Time.time + Random.Range(MinimumInterval, MaximumInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= NextSpawn && asteroidCount < MaxAsteroids && manager.game.GameOver == false)
        {
            SpawnAsteroid();
            NextSpawn = Time.time + Random.Range(MinimumInterval, MaximumInterval);
        }
    }

    void SpawnAsteroid()
    {
        float width = PlayArea.instance.bounds.width;
        float height = PlayArea.instance.bounds.height;

        float scale = Random.Range(1,5);
        Vector3 position = new Vector3(Random.Range(0,width)-width*0.5f,Random.Range(0,height)- height*0.5f);

        float DistanceToShip = Vector3.Distance(ship.transform.position, position);
        if(DistanceToShip < scale + 1)
        {   
            Vector3 addedDistance = Random.insideUnitCircle.normalized * (scale + 1);
            position += ship.transform.position + addedDistance;
        }

        GameObject asteroid = Instantiate(Asteroid,position, Quaternion.Euler(0,0,0));
        asteroid.transform.localScale = Vector3.one * scale;
    }


}
