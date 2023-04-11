using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

using UnityEngine.SceneManagement;

//A singleton class containing data and methods used by multiple systems between scenes
[RequireComponent(typeof(HighScoreManager))]
public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public Game game;

    public HighScoreManager highScoreManager;

    public scoreboard Scoreboard;
    public GameUI gameUI;

    
    // Start is called before the first frame update
    void Awake()
    {
        game = new Game();
        Debug.Log(game != null);

        if (!instance)
        {
            instance = this;
            highScoreManager = GetComponent<HighScoreManager>();

            highScoreManager.highscores = highScoreManager.LoadScores();
            game.OnGameOver += GameOver;

        }
        else Destroy(this);
    }

    void Start()
    {
        game.GameStart();
        Scoreboard.gameObject.SetActive(false);
    }

    void GameOver()
    {   
        Scoreboard.gameObject.SetActive(true);
        gameUI.gameObject.SetActive(false);
        highScoreManager.updateScores(game.score);
        highScoreManager.StoreScores(highScoreManager.highscores);
        scoreboard.Instance.ListScores(highScoreManager.highscores,highScoreManager.newHighScoreIndex);

        StartCoroutine(ReloadSceneUponButtonPress());
    }

    IEnumerator ReloadSceneUponButtonPress()
    {
        
        yield return new WaitUntil(()=>Input.GetButtonDown("Start"));
        SceneManager.LoadScene(0,LoadSceneMode.Single);
    }
}
