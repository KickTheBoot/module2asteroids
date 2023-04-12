using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreBoard : MonoBehaviour
{
    public TemplateContainer Container;

    [SerializeField]
    [Tooltip("This is the name of the visual element you want to put the score list in")]
    string HighScoreContainerName;

    [SerializeField]
    [Tooltip("This is the class name that will be added to the text element displaying the new high score")]
    string NewHighScoreClass;

    [SerializeField]
    UIDocument doc;

    int[] scores;
    // Start is called before the first frame update
    void OnEnable()
    {


        
    }

    //Displays a scoreboard, 
    public void ListScores(int[] scores, int NewHighScoreIndex)
    {
        VisualElement board = doc.rootVisualElement.Q(name: HighScoreContainerName);

        board.Clear();

        for(int i = 0; i < scores.Length; i++)
        {
            TextElement score = new TextElement();
            if(i == NewHighScoreIndex)score.AddToClassList(NewHighScoreClass);
            score.text = $"{i+1}:\t{scores[i]}";
            board.Add(score);
        }
    }


}
