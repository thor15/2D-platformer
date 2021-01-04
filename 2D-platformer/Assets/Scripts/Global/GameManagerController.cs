using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public enum PartOfLevel
{
    Ground = 0,
    Wall = 1,
    Portal = 2,
    Spike = 3
}

public class GameManagerController : MonoBehaviour
{
    #region Maneging Level
    public MovementScript player;
    public ScoreCounter counter;
    #endregion

    private Rigidbody playerRB;
    public GameObject playerGameObject;

    public GameObject mainMenu;
    public Button pauseButton;
    public Text pauseButtonText;
    private bool isPaused = false;

    public GameObject tutorailObject;

    public GameObject levelSelectMenu;
    public Button backToMain;

    private RandomGameManager randomGameManager;

    /*public GameObject level1;
    public GameObject level2;*/


    /*public Dictionary<int, List<Vector3>> coinPositions = new Dictionary<int, List<Vector3>>();
    private int index = 1;*/


    #region Building Blocks for Levels
    public ListofLevels listOfLevel = new ListofLevels();
    public List<GameObject> partsOfLevels = new List<GameObject>();
    public GameObject coin;
    public int selectedLevel = 0;
    private int currentLevel = 0;
    public List<PartOfLevel> currentGroundEnum;
    public List<Vector3> currentGroundPosistions;
    public List<Vector3> currentCoinPositions;
    #endregion

    #region Removing Level
    private List<GameObject> objectsToRemove = new List<GameObject>();
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        /*coinPositions.Add(1, new List<Vector3>());
        coinPositions.Add(2, new List<Vector3>());
        coinPositions[1].Add(new Vector3(22, 0, 0));
        coinPositions[1].Add(new Vector3(11, 0, 0));
        coinPositions[2].Add(new Vector3(22, 0, 0));
        coinPositions[2].Add(new Vector3(12, 1, 0));*/
        LoadLevels();
        playerRB = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && player.canEndLevel && selectedLevel < 15)
        {
            selectedLevel++;
            if (selectedLevel > currentLevel)
            {
                currentLevel++;
            }
            player.canEndLevel = false;
            counter.count = false;
            counter.gameObject.SetActive(false);
            RemoveLevel();
            createLevel();
            player.gameObject.transform.position = new Vector3(0, 0, 0);

        }

        if (player.transform.position.y < -10 || Input.GetKeyDown(KeyCode.Space) || player.touchingSpike)
        {
            player.transform.position = new Vector3(0, 0, 0);
            playerRB.velocity = new Vector3(0, 0, 0);
            player.touchingSpike = false;
        }
    }
    
    private void createLevel()
    {
        player.transform.position = new Vector3(0, 0,0);
        playerRB.velocity = new Vector3(0, 0, 0);
        counter.gameObject.SetActive(true);
        if (selectedLevel < listOfLevel.levelList.Count)
        {
            currentGroundEnum = listOfLevel.levelList[selectedLevel].grounds.retrieveGroundList();
            currentGroundPosistions = listOfLevel.levelList[selectedLevel].grounds.retrieveVectorList();
            currentCoinPositions = listOfLevel.levelList[selectedLevel].coins.retrieveCoinPositions();
            for (int i = 0; i < currentGroundEnum.Count; i++)
            {
                GameObject gameObject = Instantiate(partsOfLevels[(int)currentGroundEnum[i]], currentGroundPosistions[i], Quaternion.identity);
                objectsToRemove.Add(gameObject);
            }
            for (int i = 0; i < currentCoinPositions.Count; i++)
            {
                GameObject gameObject = Instantiate(coin, currentCoinPositions[i], Quaternion.identity);
                objectsToRemove.Add(gameObject);
            }
        }
    }

    public void playLevel()
    {
        if (selectedLevel == 15)
        {
            randomGameManager = FindObjectOfType<RandomGameManager>();
            randomGameManager.gameObject.SetActive(true);
        }
        levelSelectMenu.SetActive(false);
        playerGameObject.SetActive(true);
        if (selectedLevel != 0)
        {
            tutorailObject.SetActive(false);
        }
        pauseButton.gameObject.SetActive(true);
        createLevel();
        backToMain.gameObject.SetActive(false);

    }

    private void RemoveLevel()
    {
        for(int i = 0; i < objectsToRemove.Count; i++)
        {
            Destroy(objectsToRemove[i]);
        }
    }

    private void OnApplicationQuit()
    {
        SaveLevels();
    }


    private void SaveLevels()
    {
        string levelListThing = JsonUtility.ToJson(listOfLevel);
        FileManager.WriteToFile("LevelList.dat", levelListThing);
        PlayerData playerData = new PlayerData();
        playerData.coins = player.coinCount;
        playerData.lastLevel = currentLevel;
        string coins = JsonUtility.ToJson(playerData);
        FileManager.WriteToFile("PlayerData.dat", coins);
    }

    private void LoadLevels()
    {
        FileManager.LoadFromFile("LevelList.dat", out var json);
        JsonUtility.FromJsonOverwrite(json, listOfLevel);
        PlayerData playerData = new PlayerData();
        FileManager.LoadFromFile("PlayerData.dat", out var coins);
        JsonUtility.FromJsonOverwrite(coins, playerData);
        player.coinCount = playerData.coins;
        currentLevel = playerData.lastLevel;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Continue()
    {
        selectedLevel = currentLevel;
        if(selectedLevel != 0)
        {
            tutorailObject.SetActive(false);
        }
        DisableMainMenu();
        createLevel();
        pauseButton.gameObject.SetActive(true);
        playerGameObject.SetActive(true);
    }

    public void DisableMainMenu()
    {
        mainMenu.SetActive(false);
    }

    public void PuaseandResume()
    {
        if(isPaused)
        {
            Time.timeScale = 1;
            pauseButtonText.text = "Pause";
            backToMain.gameObject.SetActive(false);
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            pauseButtonText.text = "Resume";
            backToMain.gameObject.SetActive(true);
            isPaused = true;
        }
    }

    public void EnableSelect()
    {
        backToMain.gameObject.SetActive(true);
        levelSelectMenu.SetActive(true);
         for(int i = 0; i < 10; i++)
         {
             if(i >= currentLevel)
             {
                 levelSelectMenu.transform.GetChild(i).gameObject.SetActive(false);
             }   
         }
    }

    public void BackToMainMenu()
    {
        RemoveLevel();
        Time.timeScale = 1;
        isPaused = false;
        levelSelectMenu.SetActive(false);
        player.gameObject.SetActive(false);
        backToMain.gameObject.SetActive(false);
        counter.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        pauseButtonText.text = "Pause";
        mainMenu.SetActive(true);
    }
}
