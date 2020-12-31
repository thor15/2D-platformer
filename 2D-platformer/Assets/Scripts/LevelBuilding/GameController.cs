using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public MovementScript player;
    private GameManagerController gameManagerController;
    private List<GameObject> objectsToRemove = new List<GameObject>();
    private Rigidbody playerRB;
    public int selectedLevel;
    public InputField correctLevel;
    public GameObject menu;
    public Button compileLevel;
    public Button startLevel;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerController = FindObjectOfType<GameManagerController>();
        playerRB = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && player.canEndLevel)
        {
            compileLevel.gameObject.SetActive(true);
            startLevel.gameObject.SetActive(false);
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
        player.transform.position = new Vector3(0, 0, 0);
        playerRB.velocity = new Vector3(0, 0, 0);
        

        if (selectedLevel < gameManagerController.listOfLevel.levelList.Count)
        {
            gameManagerController.currentGroundEnum = gameManagerController.listOfLevel.levelList[selectedLevel].grounds.retrieveGroundList();
            gameManagerController.currentGroundPosistions = gameManagerController.listOfLevel.levelList[selectedLevel].grounds.retrieveVectorList();
            gameManagerController.currentCoinPositions = gameManagerController.listOfLevel.levelList[selectedLevel].coins.retrieveCoinPositions();
            for (int i = 0; i < gameManagerController.currentGroundEnum.Count; i++)
            {
                GameObject gameObject = Instantiate(gameManagerController.partsOfLevels[(int)gameManagerController.currentGroundEnum[i]], gameManagerController.currentGroundPosistions[i], Quaternion.identity);
                objectsToRemove.Add(gameObject);
            }
            for (int i = 0; i < gameManagerController.currentCoinPositions.Count; i++)
            {
                GameObject gameObject = Instantiate(gameManagerController.coin, gameManagerController.currentCoinPositions[i], Quaternion.identity);
                objectsToRemove.Add(gameObject);
            }
        }
    }

    public void TakeInValue(string levelSelected)
    {
        selectedLevel = (int)Int64.Parse(correctLevel.text);
        HideMenu();
        createLevel();
    }

    public void CreateNewLevel()
    {
        HideMenu();
        selectedLevel = gameManagerController.listOfLevel.levelList.Count;
    }

    private void HideMenu()
    {
        menu.SetActive(false);
        startLevel.gameObject.SetActive(true);
    }

    public void onClick()
    {
        player.gameObject.SetActive(true);
    }
}
