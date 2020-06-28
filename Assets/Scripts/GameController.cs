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
    public GameObject bike;
    private GameObject pathAnimationInstance;
    private UnityAction endGame;
    private UnityAction pathEndAction;
    private GameModel gameModel;
    private int collectedCoins;
    private int totalCoins;
    // Start is called before the first frame update
    void Start()
    {
        bike.SetActive(false);
        bonusSpawner.enabled = false;
        collectedCoins = 0;
        vehicleTextField.text = "Kick scooter";
        endGame += endGameHandler;
        pathEndAction += orderSuccess;
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

    public void orderSuccess()
    {
        if(gameModel.GetState() == GameModel.States.PathGo) {
            gameModel.NewOrder();
            bike.SetActive(false);
            paralaxBack.SetActive(false);
            bonusSpawner.enabled = false;
            map.transform.localScale = new Vector3(map.transform.localScale.x * 2f, map.transform.localScale.y * 2f, map.transform.localScale.z);
            GameObject.Destroy(pathAnimationInstance);
            map.SetActive(true);
        }
    }

    public void PathGo()
    {
        if (gameModel.GetState() == GameModel.States.PathBuild)
        {
            bike.SetActive(true);
            gameModel.StartPathGo();
            bonusSpawner.enabled = true;
            paralaxBack.SetActive(true);
            map.transform.localScale = new Vector3(map.transform.localScale.x * 0.5f, map.transform.localScale.y * 0.5f, map.transform.localScale.z);
            Vector3 pathGoPosition = new Vector3(map.transform.position.x - 0.45f, map.transform.position.y + map.GetComponent<Map>().Rows * 0.25f, transform.position.z);
            pathAnimationInstance = Instantiate(pathAnimation, pathGoPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
            pathAnimationInstance.GetComponent<PathGo>().SetPath(map.GetComponent<Map>().path);
            pathAnimationInstance.GetComponent<PathGo>().SetStartPoint(map.GetComponent<Map>().StartPointCoords);
            pathAnimationInstance.GetComponent<PathGo>().SetPathEndAction(pathEndAction);
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
