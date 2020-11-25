using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public MovementScript player;
    public ScoreCounter counter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && player.canEndLevel)
        {
            Debug.Log("level over");
            counter.count = false;
        }

    }
}
