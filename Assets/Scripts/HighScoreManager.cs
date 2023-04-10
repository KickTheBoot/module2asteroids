using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;

using System.IO;
using System.Linq;


public class HighScoreManager : MonoBehaviour
{

    public int[] highscores;


    void Start()
    {
        highscores = loadScores();
        sortScores();
        StartCoroutine(WaitForScoreBoardThenDisplayScores());
    }

    public IEnumerator WaitForScoreBoardThenDisplayScores()
    {
        yield return new WaitUntil(() => scoreboard.Instance);
        scoreboard.Instance.ListScores(highscores);
    }



    //sorts the scores in descending order with insertion sort
    public void sortScores()
    {
        for (int i = 0; i < highscores.Length; i++)
        {
            int j = i;
            while (j > 0 && highscores[j - 1] < highscores[j])
            {
                //Swap the values
                int k = highscores[j - 1];
                highscores[j - 1] = highscores[j];
                highscores[j] = k;

                j--;
            }
        }

    }

    public void updateScores(int score)
    {
        for (int i = 0; i < highscores.Length; i++)
        {
            //check if the current score is greater than the comparable one
            if (score > highscores[i])
            {

                int j = highscores.Length - 1;
                Debug.Log($"HS:{highscores.Length} J:{j}");
                while (j > i)
                {
                    Debug.Log($"J: {j}");
                    highscores[j] = highscores[j - 1];
                    j--;
                }
                highscores[i] = score;
                return;
            }
        }
    }


    //store the high scores in a file
    void StoreScores(int[] scores)
    {
        string path = Application.dataPath + "/scores.dat";

        FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);

        if (fs.CanWrite)
        {
            byte[] data = new byte[scores.Length * sizeof(int)];

            //iterate through the integer, convert them into bytes and store the converted bytes in the data buffer
            for (int i = 0; i < scores.Length; i++)
            {
                Debug.Log(i);
                byte[] current = BitConverter.GetBytes(scores[i]);
                Debug.Log(BitConverter.ToInt32(current));
                for (int j = 0; j < sizeof(int); j++)
                {
                    data[i * sizeof(int) + j] = current[j];
                }
            }

            fs.Write(data);

        }

        fs.Flush();
        fs.Close();

    }

    //Load the high scores from a file
    public int[] loadScores()
    {
        //in essence this file just returns an array of integers from a file. It might be a good idea to make a less specific version of this function for later use
        string path = Application.dataPath + "/scores.dat";
        Debug.Log(path);

        if (File.Exists(path))
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            //Create the returnable array
            int[] scores = new int[(fs.Length / sizeof(int))];
            byte[] buffer = new byte[fs.Length];


            fs.Read(buffer, 0, buffer.Length);

            //iterate through the bytes in the array in 4 byte chunks and convert them to integers
            for (int i = 0; i < scores.Length; i++)
            {
                byte[] current = new byte[sizeof(int)];
                for (int j = 0; j < sizeof(int); j++)
                {
                    current[j] = buffer[i * sizeof(int) + j];
                }
                scores[i] = BitConverter.ToInt32(current);
                Debug.Log(scores[i]);
            }

            fs.Flush();
            fs.Close();
            return scores;
        }

        else return new int[8];
    }

}