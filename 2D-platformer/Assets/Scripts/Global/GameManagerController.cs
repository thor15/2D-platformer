using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum PartOfLevel
{
    Ground = 0,
    Wall = 1,
    Portal = 2
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

    public GameObject tutorailObject;

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
    private List<PartOfLevel> currentGroundEnum;
    private List<Vector3> currentGroundPosistions;
    private List<Vector3> currentCoinPositions;
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
        if (Input.GetKeyDown(KeyCode.S) && player.canEndLevel)
        {
            selectedLevel++;
            if(selectedLevel > currentLevel)
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

        if (player.transform.position.y < -10 || Input.GetKeyDown(KeyCode.Space))
        {
            player.transform.position = new Vector3(0, 0, 0);
            playerRB.velocity = new Vector3(0, 0, 0);
        }


    }
    
    private void createLevel()
    {
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
        playerGameObject.SetActive(true);
    }

    private void DisableMainMenu()
    {
        mainMenu.SetActive(false);
    }
}
