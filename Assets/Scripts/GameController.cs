using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public Text timerText;
    public GameObject endGameUI;
    public Text collectedCoinsTextField;
    public Text totalCoinsTextField;
    public Text vehicleTextField;
    public GameObject paralaxBack;
    public GameObject map;
    public SpawnBonuses bonusSpawner;
    private UnityAction endGame;
    private GameModel gameModel;
    private int collectedCoins;
    private int totalCoins;
    // Start is called before the first frame update
    void Start()
    {
        bonusSpawner.enabled = false;
        collectedCoins = 0;
        vehicleTextField.text = "Kick scooter";
        endGame += endGameHandler;
        endGameUI.SetActive(false);
        paralaxBack.SetActive(false);
        gameModel = new GameModel(15000);
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
            paralaxBack.SetActive(false);
            bonusSpawner.enabled = false;
            map.SetActive(true);
        }
    }

    public void PathGo()
    {
        if (gameModel.GetState() == GameModel.States.PathBuild)
        {
            gameModel.StartPathGo();
            bonusSpawner.enabled = true;
            paralaxBack.SetActive(true);
            map.SetActive(false);
        }
    }

    public void Coin()
    {
        collectedCoins++;
    }

    public void endGameHandler()
    {
        endGameUI.SetActive(true);
        bonusSpawner.enabled = false;
        collectedCoinsTextField.text = "Collected coins: " + collectedCoins.ToString();
    }

    public void SetBicycle()
    {
        vehicleTextField.text = "Bycicle";
    }
}
