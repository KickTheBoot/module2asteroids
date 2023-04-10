using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class scoreboard : MonoBehaviour
{
    public static scoreboard Instance;
    public TemplateContainer Container;
    UIDocument doc;

    int[] scores;
    // Start is called before the first frame update
    void Start()
    {
        if(!Instance)
        Instance = this;

        doc = GetComponent<UIDocument>();
        
    }

    public void ListScores(int[] scores)
    {
        VisualElement board = doc.rootVisualElement.Q(name: "ScoreContainer");

        board.Clear();

        for(int i = 0; i < scores.Length; i++)
        {
            TextElement score = new TextElement();
            score.text = $"{i+1}: {scores[i]}";
            board.Add(score);
        }
    }

}
