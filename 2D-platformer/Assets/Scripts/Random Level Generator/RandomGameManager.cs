using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGameManager : MonoBehaviour
{
    public MovementScript player;
    public Rigidbody playerRB;

    private RandomLevelGenerator levelGenerator;

    public List<GameObject> partsOfLevels = new List<GameObject>();
    public List<PartOfLevel> currentGroundEnum;
    public List<Vector3> currentGroundPosistions;
    public List<Vector3> currentCoinPositions;

    private List<GameObject> objectsToRemove = new List<GameObject>();

    // Start is called before the first frame update
    void Awake()
    {
        levelGenerator = new RandomLevelGenerator(player.playerSpeed, player.jumpHeight, this);
        levelGenerator.RandomlyCreateLevel();
        CreateRandomLevel();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && player.canEndLevel)
        {
            RemoveLevel();
            levelGenerator.RandomlyCreateLevel();
            CreateRandomLevel();
        }

        if (/*player.transform.position.y < -10 ||*/ Input.GetKeyDown(KeyCode.Space) || player.touchingSpike)
        {
            player.transform.position = new Vector3(0, 0, 0);
            playerRB.velocity = new Vector3(0, 0, 0);
            player.touchingSpike = false;
        }
    }


    private void CreateRandomLevel()
    {
        player.transform.position = new Vector3(0, 0, 0);
        playerRB.velocity = new Vector3(0, 0, 0);
        for (int i = 0; i < currentGroundEnum.Count; i++)
        {
            GameObject gameObject = Instantiate(partsOfLevels[(int)currentGroundEnum[i]], currentGroundPosistions[i], Quaternion.identity);
            objectsToRemove.Add(gameObject);
        }
        /*for (int i = 0; i < currentCoinPositions.Count; i++)
        {
            GameObject gameObject = Instantiate(coin, currentCoinPositions[i], Quaternion.identity);
            objectsToRemove.Add(gameObject);
        }*/
    }


    public void RemoveLevel()
    {
        for (int i = 0; i < objectsToRemove.Count; i++)
        {
            Destroy(objectsToRemove[i]);
        }
        currentGroundPosistions = new List<Vector3>();
        currentGroundEnum = new List<PartOfLevel>();
    }

    public void BuildNewLevel()
    {
        RemoveLevel();
        levelGenerator.RandomlyCreateLevel();
        CreateRandomLevel();
    }
}
