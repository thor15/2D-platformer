using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    private int score = 0;
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        score = 10000;
    }

    // Update is called once per frame
    void Update()
    {
        score -= (int)(500 * Time.deltaTime);
        scoreText.text = ("Score: "+score);
    }
}
