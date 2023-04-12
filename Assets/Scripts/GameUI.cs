using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameUI : MonoBehaviour
{   
    [SerializeField]
    string ScoreName, LifeCountName;
    UIDocument doc;
    
    TextElement ScoreText;

    TextElement LifeCount;
    // Start is called before the first frame update
    void Start()
    {
        doc = GetComponent<UIDocument>();

        ScoreText = doc.rootVisualElement.Q<TextElement>(ScoreName);
        LifeCount = doc.rootVisualElement.Q<TextElement>(LifeCountName);
        
        GameManager.instance.game.OnScoreUpdate += UpdateScore;
        GameManager.instance.game.OnLivesUpdated += UpdateLives;

        UpdateLives(GameManager.instance.game.Lives);

    }

    // Update is called once per frame
    void UpdateScore(int amount)
    {
        ScoreText.text = $"Score:\t{amount}";
    }

    void UpdateLives(int amount)
    {
        LifeCount.text = $"Lives: {amount}";
    }
}
