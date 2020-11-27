﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerController : MonoBehaviour
{
    public MovementScript player;
    public ScoreCounter counter;
    public GameObject level1;
    public GameObject level2;
    public GameObject coin;
    public List<List<Vector3>> coinPositions = new List<List<Vector3>>();
    private int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        CreateCoins();
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
            CreateCoins();
            player.gameObject.transform.position = new Vector3(0, 0, 0);
            counter.gameObject.SetActive(true);

        }

        if(player.transform.position.y < -10)
        {
            player.transform.position = new Vector3(0, 0, 0);
        }


    }
    
    void CreateCoins()
    {
        for (int i = 0; i < coinPositions[index].Count; i++)
        {
            Instantiate(coin, coinPositions[index][i], Quaternion.identity);
        }
        index++;
    }
}
