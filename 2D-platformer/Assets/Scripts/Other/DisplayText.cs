using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayText : MonoBehaviour
{

    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        MovementScript.DisableText += ShowText;
        //StartCoroutine(ExampleCoroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }



    private void ShowText()
    {
        text.gameObject.SetActive(true);
        StartCoroutine(timeToDisable());
    }

    void OnDisable()
    {

    }

    IEnumerator timeToDisable()
    {
        //Debug.LogError("Running IENUMERATOR");
        yield return new WaitForSeconds(5);
        //Debug.LogError("Text is False");
        text.text = "There are coins that you can pick up by running into them.";
        StartCoroutine(ExampleCoroutine());
    }


    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        text.text = "This is a Portal, you can click \"s\" to end the level"; 
        //After we have waited 5 seconds print the time again.
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        StartCoroutine(AnotherEnumerator());
    }

    IEnumerator AnotherEnumerator()
    {
        //Debug.LogError("Running AnotherEnumerator");
        yield return new WaitForSeconds(5);
        text.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }


}
