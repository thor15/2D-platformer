using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    private int score = 0;
    public TextMeshProUGUI scoreText;
    public bool count;
 
    void OnEnable()
    {
        score = 10000;
        count = true;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (count)
        {
            score -= (int)(500 * Time.deltaTime);
            scoreText.text = ("Score: " + score);
        }
    }

    void OnDisable()
    {

    }
}
