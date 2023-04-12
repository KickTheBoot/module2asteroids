using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game
{

    public int Lives;

    public int Score;

    public bool GameOver = false;

    public void UpdateLives(int amount)
    {
        Lives = amount;
        if(OnLivesUpdated != null)OnLivesUpdated.Invoke(Lives);
    }

    public void GameStart()
    {
        UpdateLives(3);
        if(OnGameStart != null)OnGameStart.Invoke();
    }

    public void Death()
    {   
        if(OnDeath != null)OnDeath.Invoke();
        //Game over
        if(Lives == 0) 
        {
            GameOver = true;
            Debug.Log("Game Over");
            OnGameOver.Invoke();
        }
        else
        {
            if(OnDeath != null)OnDeath.Invoke();
            UpdateLives(Lives - 1);
            Debug.Log("Dead");
        }
        

    }

    public void AddScore(int amount)
    {
        score += amount;
        if(OnScoreUpdate != null)OnScoreUpdate.Invoke(score);
    }


    public int score;

    public delegate void eventhandler();
    public event eventhandler OnDeath;
    public event eventhandler OnGameOver;
    public event eventhandler OnGameStart;

    public delegate void ValueUpdateHandler(int currentScore);
    public event ValueUpdateHandler OnScoreUpdate;
    
    public event ValueUpdateHandler OnLivesUpdated;

}
