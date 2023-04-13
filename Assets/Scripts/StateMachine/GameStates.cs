using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateMachine
{
    IState m_state;

    MonoBehaviour owner;

    public StateMachine(MonoBehaviour owner)
    {
        this.owner = owner;
    }

    public void SetState(IState state)
    {
        if(m_state != null)
        {
            //Unsubscribe from the current state's StateMessage
            m_state.OnStateMessage -= StateMessage;
            m_state.Exit();
        }

        //Set the new state
        m_state = state;

        //Enter the state
        m_state.Enter();
    }

    public void SetState(string StateName)
    {
        if(m_state != null)
        {
            //Unsubscribe from the current state's StateMessage
            m_state.OnStateMessage -= StateMessage;
            m_state.Exit();
        }

        //Set the new state
        m_state = GameStateFactory.Create(StateName);

        m_state.OnStateMessage += StateMessage;
        m_state.Enter();
    }

    public void ExecuteState()
    {
        m_state.Execute();
    }

    void StateMessage(string message)
    {
        string[] arguments = message.Split(' ');
        string Command = arguments[0];

        switch(Command)
        {
            //Changes the state to t
            //Format: Change [Statename]
            case "Change":
            SetState(arguments[1]);
            break;
            //Format:  SendMessage [Message]
            case "SendMessage":
            owner.SendMessage(arguments[1]);
            break;
            default:
            Debug.Log($"Invalid command '{Command}'");
            break;


        }

    }
}

public delegate void StateMessage(string arguments);

public static class GameStateFactory
{
    //Factory method for creating a state matching its associated string
    public static IState Create(string Name)
    {
        switch(Name)
        {
            default:
            return new NullState();
            case "gameOver":
            return new GameOverState();
            case "startScreen":
            return new StartScreenState();
            case "gamePlay":
            return new GamePlay();
            case "scoreBoard":
            return new ScoreBoardState();
        }
    }
    
}


//An interface for a state
public interface IState
{
    //Event used for communicating with the state machine and it's parent gameObject
    public event StateMessage OnStateMessage;

    public abstract void Enter();

    public abstract void Execute();

    public abstract void Exit();
}




public class NullState : IState
{

    public event StateMessage OnStateMessage;

    public void Enter()
    {

    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
}

public class StartScreenState : IState
{

    public event StateMessage OnStateMessage;

    public void Enter()
    {

    }

    public void Execute()
    {
        OnStateMessage.Invoke("SendMessage OnStartScreen");
    }

    public void Exit()
    {

    }
}

public class GamePlay : IState
{

    public event StateMessage OnStateMessage;

    public void Enter()
    {

    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
}

public class GameOverState : IState
{
    public event StateMessage OnStateMessage;
    public void Enter()
    {

    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
}

public class ScoreBoardState : IState
{
    public event StateMessage OnStateMessage;

    public void Enter()
    {

    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
}