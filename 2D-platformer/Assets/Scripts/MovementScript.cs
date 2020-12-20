using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


public class MovementScript : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private Rigidbody personRB;
    private bool isOnGround;
    private Vector3 offest;
    public bool canEndLevel = false;
    public int coinCount = 0;
    public float playerSpeed = 3.0f;
    public static event Action DisableText;

    // Start is called before the first frame update
    void Start()
    {
        personRB = GetComponent<Rigidbody>();
        isOnGround = true;
        offest = mainCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * playerSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.W) && isOnGround)
        {
            personRB.AddForce(Vector3.up * 10, ForceMode.Impulse);
            isOnGround = false;
        }
        
        mainCamera.transform.position = transform.position + offest;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Portal"))
        {
            canEndLevel = true;
        }

        if(other.CompareTag("Coin"))
        {
            coinCount++;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Tutorial"))
        {
            if(DisableText != null)
            {
                DisableText();
            }
            other.gameObject.SetActive(false);
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Portal"))
        {
            canEndLevel = false;
        }
    }

    
}
