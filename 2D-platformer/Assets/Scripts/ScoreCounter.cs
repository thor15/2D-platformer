using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        score = 10,000;
    }

    // Update is called once per frame
    void Update()
    {
        score = score - 10;
    }
}
