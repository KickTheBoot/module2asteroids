using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A singleton class containing data and methods used by multiple systems between scenes
public class GameManager : MonoBehaviour
{
    public struct highScore
    {
        char[] holder;
        //the 
        int score;
    }
    //The access point for the singleton
    public static GameManager game;
    
    //The score o
    public int Score;


    public int Lives;

    // Start is called before the first frame update
    void Start()
    {
        
        if(!game)
        {
            game = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GameOver()
    {

    }
}
