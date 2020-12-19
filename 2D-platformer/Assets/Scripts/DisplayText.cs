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

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDisable()
    {
        text.gameObject.SetActive(true);
        StartCoroutine(timeToDisable());
    }

    IEnumerator timeToDisable()
    {
        yield return new WaitForSeconds(10);
        text.gameObject.SetActive(false);
    }
}
