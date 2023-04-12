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

    [SerializeField]
    ScoreBoard scoreboard;
    public GameUI gameUI;

    
    // Start is called before the first frame update
    void Awake()
    {
        game = new Game();
        Debug.Log(game != null);

        if(!scoreboard)
        {
            scoreboard = FindObjectOfType<ScoreBoard>();
        }

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
        scoreboard.gameObject.SetActive(false);
    }

    void GameOver()
    {   
        scoreboard.gameObject.SetActive(true);
        gameUI.gameObject.SetActive(false);
        highScoreManager.updateScores(game.score);
        highScoreManager.StoreScores(highScoreManager.highscores);
        scoreboard.ListScores(highScoreManager.highscores,highScoreManager.newHighScoreIndex);

        StartCoroutine(ReloadSceneUponButtonPress());
    }

    IEnumerator ReloadSceneUponButtonPress()
    {
        
        yield return new WaitUntil(()=>Input.GetButtonDown("Start"));
        SceneManager.LoadScene(0,LoadSceneMode.Single);
    }
}
