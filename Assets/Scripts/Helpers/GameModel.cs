using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

public class GameModel : ScriptableObject
{
    private int buildModeMultiplier = 3;
    private int timeBonus = 10000;
    private UnityAction endGame;

    public void newGame(int initTimer)
    {
        gameTimer = initTimer;
        state = States.PathBuild;
    }

    public enum States
    {
        PathBuild,
        PathGo,
        GameOver,
    }

    private static States state = States.PathBuild;
    private static int gameTimer;

    public void StartPathBuild()
    {
        state = States.PathBuild;
    }

    public void StartPathGo()
    {
        if (state != States.PathBuild)
        {
            return;
        }
        state = States.PathGo;
    }

    public States GetState()
    {
        return state;
    }

    public int count(int delay)
    {
        if (gameTimer <= 0)
        {
            state = States.GameOver;
            endGame();
        }
        if (state == States.PathBuild)
        {
            gameTimer -= Mathf.CeilToInt(delay / buildModeMultiplier);
        }
        else
        {
            gameTimer -= delay;
        }
        return gameTimer;
    }

    public void NewOrder()
    {
        state = States.PathBuild;
        gameTimer += timeBonus;
    }

    public void setEndGameAction(UnityAction action)
    {
        endGame += action;
    }
}