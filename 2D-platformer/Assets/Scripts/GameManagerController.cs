using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PartOfLevel
{
    Ground = 0,
    Wall = 1,
    Portal = 2
}

public class GameManagerController : MonoBehaviour
{
    public List<GameObject> partsOfLevels = new List<GameObject>();

    //public ListOfLevels levelList = new ListOfLevels();

    public MovementScript player;
    public ScoreCounter counter;
    
    public GameObject level1;
    public GameObject level2;
    
    public GameObject coin;
    public Dictionary<int, List<Vector3>> coinPositions = new Dictionary<int, List<Vector3>>();
    private int index = 1;

    // Start is called before the first frame update
    void Start()
    {
        coinPositions.Add(1, new List<Vector3>());
        coinPositions.Add(2, new List<Vector3>());
        coinPositions[1].Add(new Vector3(22, 0, 0));
        coinPositions[1].Add(new Vector3(11, 0, 0));
        coinPositions[2].Add(new Vector3(22, 0, 0));
        coinPositions[2].Add(new Vector3(12, 1, 0));
        createCoins();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && player.canEndLevel)
        {
            
            counter.count = false;
            counter.gameObject.SetActive(false);
            level1.SetActive(false);
            level2.SetActive(true);
            createCoins();
            player.gameObject.transform.position = new Vector3(0, 0, 0);
            counter.gameObject.SetActive(true);

        }

        if(player.transform.position.y < -10)
        {
            player.transform.position = new Vector3(0, 0, 0);
        }


    }
    
    void createCoins()
    {
        if (index <= coinPositions.Count)
        {
            for (int i = 0; i < coinPositions.Count; i++)
            {
                Instantiate(coin, coinPositions[index][i], Quaternion.identity);
            }
            index++;
        }
    }
}
