using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

//A singleton class containing data and methods used by multiple systems between scenes
[RequireComponent(typeof(HighScoreManager))]
public class GameManager : MonoBehaviour
{
    
    public InputActionAsset controls;

    InputAction start, quit;
    public static GameManager instance;

    public Game game;

    public HighScoreManager highScoreManager;

    [SerializeField]
    ScoreBoard scoreboard;
    public GameUI gameUI;

    
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(CleanUp());
        instance = this;
        

        InputActionMap Controls = controls.FindActionMap("Controls");

        start = Controls.FindAction("Start");
        quit = Controls.FindAction("quit");

        game = new Game();
        
        

        quit.performed += ctx => {Quit(ctx);};

        start.Enable();
        quit.Enable();

        Debug.Log(game != null);

        if(!scoreboard)
        {
            scoreboard = FindObjectOfType<ScoreBoard>();
        }


        game.OnScoreUpdate += gameUI.UpdateScore;
        game.OnLivesUpdated += gameUI.UpdateLives;  

        Debug.Log("instance set");
            highScoreManager = GetComponent<HighScoreManager>();

            highScoreManager.highscores = highScoreManager.LoadScores();
            game.OnGameOver += GameOver;

       

            
            scoreboard.gameObject.SetActive(false);
    }


    public void Start()
    {          
        game.GameStart();
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

    public void Quit(InputAction.CallbackContext context)
    {
        Application.Quit();
    }

    IEnumerator ReloadSceneUponButtonPress()
    {
        
        yield return new WaitUntil(() => start.WasPressedThisFrame());
        SceneManager.LoadScene(0,LoadSceneMode.Single);
    }

    
    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator CleanUp()
    {
        while(true)
        {
        Asteroid[] Asteroids = FindObjectsOfType<Asteroid>();
        if(Asteroids != null)
            for(int i = 0; i < Asteroids.Length; i++)
            {
                if(!Asteroids[i].enabled)
                {
                    Destroy(Asteroids[i].gameObject);
                }
            }
            yield return new WaitForSeconds(5);
        }
    }
}
