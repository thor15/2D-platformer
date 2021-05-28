﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


public class MovementScript : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private Rigidbody personRB;
    private bool isOnGround;
    public int jump = 1;
    private Vector3 offest;
    public bool canEndLevel = false;
    public int coinCount = 0;
    public float playerSpeed = 3.0f;
    public static event Action DisableText;
    public bool touchingSpike = false;
    public float jumpHeight = 10.0f;

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
        if (Input.GetKeyDown(KeyCode.W) && isOnGround && jump > 0)
        {
            personRB.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            jump--;
            isOnGround = false;
        }
                
        mainCamera.transform.position = transform.position + offest;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            jump++;
        }
    }

    /*private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Death"))
        {
            touchingSpike = true;
        }
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
            //other.gameObject.SetActive(false);
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
