using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildClassOutController : MonoBehaviour
{
    public List<GameObject> partsOfTheLevel = new List<GameObject>();
    private GameManagerController gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManagerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buildLevel()
    {
        Level newLevel = new Level();
        foreach(GameObject gameObject in partsOfTheLevel)
        {
            switch(gameObject.tag)
            {
                case "Coin":
                    newLevel.coins.addList(gameObject.transform.position);
                    break;
                case "Ground":
                    newLevel.grounds.addToList(PartOfLevel.Ground, gameObject.transform.position);
                    break;
                case "Wall":
                    newLevel.grounds.addToList(PartOfLevel.Wall, gameObject.transform.position);
                    break;
                case "Portal":
                    newLevel.grounds.addToList(PartOfLevel.Portal, gameObject.transform.position);
                    break;
            }
        }
        gameManager.listOfLevel.levelList.Add(newLevel);
    }

}
