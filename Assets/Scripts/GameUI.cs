using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameUI : MonoBehaviour
{   

    GameManager manager;
    [SerializeField]
    string ScoreName, LifeCountName;
    [SerializeField]
    UIDocument doc;
    
    TextElement ScoreText;

    TextElement LifeCount;
    // Start is called before the first frame update
    void Awake()
    {
        doc = GetComponent<UIDocument>();

        ScoreText = doc.rootVisualElement.Q<TextElement>(name:ScoreName);
        LifeCount = doc.rootVisualElement.Q<TextElement>(name:LifeCountName);
        

    }

    // Update is called once per frame
    public void UpdateScore(int amount)
    {

        ScoreText.text = $"Score:\t{amount}";
    }

    public void UpdateLives(int amount)
    {
        LifeCount.text = $"Lives: {amount}";
    }
}
