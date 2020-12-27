using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectALevel : MonoBehaviour
{

    private GameManagerController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<GameManagerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void click()
    {
        controller.DisableMainMenu();
        controller.EnableSelect();
    }
}
