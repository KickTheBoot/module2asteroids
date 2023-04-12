using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{

    public enum GameState
    {
        Init,
        GameOver,
        Playing,
        scoreboard
    }

    GameState state;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetState(GameState state)
    {
        switch(state)
        {
            case GameState.Init:
            break;

            case GameState.GameOver:

            break;
            
            case GameState.Playing:

            break;
            
            case GameState.scoreboard:

            break;
        }
    }
}
