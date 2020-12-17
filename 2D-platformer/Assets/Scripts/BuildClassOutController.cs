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
        //Level newLevel = new Level();
        foreach(GameObject gameObject in partsOfTheLevel)
        {
            switch(gameObject.tag)
            {
                case "Coin":
                    //Level.coins.addList(gameObject.transform.position);
                    break;
                case "Ground":
                    //Level.grounds.addToList(PartOfLevel.Ground, gameObject.transform.position);
                    break;
                case "Wall":
                    //Level.grounds.addToList(PartOfLevel.Wall, gameObject.transform.position);
                    break;
                case "Portal":
                    //Level.grounds.addToList(PartOfLevel.Portal, gameObject.transform.position);
                    break;
            }
        }
        //gameManager.levelList.levelList.add(newLevel);
    }

}
