using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static int asteroidCount = 0;
    [SerializeField]
    GameObject Asteroid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnAsteroid()
    {
        float width = PlayaArea.instance.bounds.width;
        float height = PlayaArea.instance.bounds.height;

        GameObject asteroid = Instantiate(Asteroid, new Vector3(Random.Range(0,width)-width*0.5f,Random.Range(0,height)- height*0.5f),Quaternion.Euler(0,0,0));
        asteroid.transform.localScale = Vector3.one * Random.Range(1,5);
    }

    void OnGUI()
    {
        if(GUILayout.Button("Spawn asteroid")) SpawnAsteroid();
    }


}
