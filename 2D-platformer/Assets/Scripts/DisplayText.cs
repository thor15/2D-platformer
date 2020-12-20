using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayText : MonoBehaviour
{

    public TextMeshProUGUI text;

    public int thisDoesNothing = 0;

    // Start is called before the first frame update
    void Start()
    {
        MovementScript.DisableText += ShowText;
    }

    // Update is called once per frame
    void Update()
    {
        thisDoesNothing++;
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
        Debug.Log("Running IENUMERATOR");
        yield return new WaitForSecondsRealtime(1);
        text.gameObject.SetActive(false);
        Debug.Log("Text is False");
        Destroy(this);
    }

    
}
