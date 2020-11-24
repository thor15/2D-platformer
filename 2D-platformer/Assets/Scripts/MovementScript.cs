using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementScript : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private Rigidbody personRB;
    private bool isOnGround;
    private Vector3 offest;
    private bool canEndLevel = false;

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
            transform.Translate(Vector3.left * 3 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * 3 * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.W) && isOnGround)
        {
            personRB.AddForce(Vector3.up * 10, ForceMode.Impulse);
            isOnGround = false;
        }
        if(Input.GetKeyDown(KeyCode.S) && canEndLevel)
        {
            Debug.Log("level over");
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
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Portal"))
        {
            canEndLevel = false;
        }
    }
}
