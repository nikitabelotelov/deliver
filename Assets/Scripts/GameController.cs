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
    public GameObject pathAnimation;
    private GameObject pathAnimationInstance;
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
        gameModel = new GameModel();
        gameModel.setEndGameAction(endGame);
        gameModel.newGame(15000);
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
            pathAnimationInstance = Instantiate(pathAnimation, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            pathAnimationInstance.GetComponent<PathGo>().SetPath(map.GetComponent<Map>().path);
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

    public void newGame()
    {
        gameModel.newGame(15000);
        endGameUI.SetActive(false);
        map.SetActive(true);
    }
}
