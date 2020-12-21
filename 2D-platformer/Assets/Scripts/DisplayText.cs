using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayText : MonoBehaviour
{

    public TextMeshProUGUI text;

    private bool displayText = false;
    // Start is called before the first frame update
    void Start()
    {
        MovementScript.DisableText += ShowText;
        //StartCoroutine(ExampleCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(displayText);
        if(displayText)
        {
            Debug.LogError("Is display text true");
            StartCoroutine(timeToDisable());
            displayText = false;
        }
    }



    private void ShowText()
    {
        Debug.LogWarning("Is this method called?");
        text.gameObject.SetActive(true);
        Debug.LogError("does it make it to here");
        displayText = true;
        Debug.LogError("ahngrieuohnuipoaerhngioAWHG[OIWEAHNGWUAHG");
    }

    void OnDisable()
    {

    }

    IEnumerator timeToDisable()
    {
        Debug.LogError("Running IENUMERATOR");
        yield return new WaitForSeconds(5);
        Debug.LogError("Text is False");
        text.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
