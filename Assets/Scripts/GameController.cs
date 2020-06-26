using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text timerText;
    private GameModel gameModel;
    // Start is called before the first frame update
    void Start()
    {
        gameModel = new GameModel(15000);
    }

    // Update is called once per frame
    void Update()
    {
        var state = gameModel.GetState();
        Debug.Log(state);
        if(state == GameModel.States.PathBuild || state == GameModel.States.PathGo)
        {
            var time = gameModel.count(Mathf.FloorToInt(Time.deltaTime * 1000));
            timerText.text = FormateTime(time);
        } else if(state == GameModel.States.GameOver)
        {
            // TODO
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
}
