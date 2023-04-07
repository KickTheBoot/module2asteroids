using System.Collections.Generic;
using System;
using UnityEngine;


public class HighScoreManager : MonoBehaviour
{

    public int[] highscores;

    public int highscoreupdater;

    void LoadHighScores()
    {

    }

    void SaveHighScores()
    {

    }

    void Awake()
    {
        sortScores();
    }

    public void OnGUI()
    {
        if(GUILayout.Button("Sort scores"))sortScores();
        if(GUILayout.Button("Update high scores"))updateScores(highscoreupdater);
    }


    //sorting the scores with insertion sort
    public void sortScores()
    {
        for(int i = 0; i < highscores.Length; i++)
        {
            int j = i;
            while(j > 0 && highscores[j-1] < highscores[j])
            {
                //Swap the values
                int k = highscores[j-1];
                highscores[j-1] = highscores[j];
                highscores[j] =k ;

                j--;
            }
        }
        
    }

    public void updateScores(int score)
    {
        for(int i = 0; i < highscores.Length; i++)
        {
            //check if the current score is greater than the one at the comparable rank
            if(score > highscores[i])
            {
                
                int j = highscores.Length - 1;
                Debug.Log($"HS:{highscores.Length} J:{j}");
                while(j> i)
                {
                    Debug.Log($"J: {j}");
                    highscores[j] = highscores[j-1];
                    j--;
                }
                highscores[i] = score;
                return;
            }
        }
    }
    
}