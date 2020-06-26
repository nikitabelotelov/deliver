using UnityEngine;
using UnityEditor;

public class GameModel : ScriptableObject
{
    private int buildModeMultiplier = 3;
    private int timeBonus = 10000;

    public GameModel(int initTimer)
    {
        SetTimer(initTimer);
    }

    public enum States
    {
        PathBuild,
        PathGo,
        GameOver,
    }

    private static States state = States.PathBuild;
    private static int gameTimer;

    // 2000ms == 2 seconds
    private static void SetTimer(int initialTimer)
    {
        gameTimer = initialTimer;
    }

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
}