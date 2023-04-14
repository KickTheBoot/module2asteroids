using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    StateMachine stateMachine;
    // Start is called before the first frame update
    void Start()
    {
        stateMachine = new StateMachine(this);
        stateMachine.SetState("startScreen");
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.ExecuteState();
    }

    public void OnStartScreen()
    {

    }
}
