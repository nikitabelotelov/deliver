using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public Text timerText;
    private UnityAction endGame;
    public GameObject endGameUI;
    private GameModel gameModel;
    // Start is called before the first frame update
    void Start()
    {
        endGame += endGameHandler;
        endGameUI.SetActive(false);
        gameModel = new GameModel(5000);
        gameModel.setEndGameAction(endGame);
    }

    // Update is called once per frame
    void Update()
    {
        var state = gameModel.GetState();
        if(state == GameModel.States.PathBuild || state == GameModel.States.PathGo)
        {
            var time = gameModel.count(Mathf.FloorToInt(Time.deltaTime * 1000));
            timerText.text = FormateTime(time);
        }
    }

    private string FormateTime(int timeRaw)
    {
        string seconds = (timeRaw / 1000).ToString();
        string mlsec = ((timeRaw % 1000) / 10).ToString();
        return seconds + ':' + mlsec;
    }

    public void Click()
    {
        if(gameModel.GetState() == GameModel.States.PathGo) {
            gameModel.NewOrder();
        }
    }

    public void PathGo()
    {
        if (gameModel.GetState() == GameModel.States.PathBuild)
        {
            gameModel.StartPathGo();
        }
    }

    public void endGameHandler()
    {
        endGameUI.SetActive(true);
    }
}
