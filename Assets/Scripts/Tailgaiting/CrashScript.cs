using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashScript : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Crashable"))
        {
            Debug.Log("Car's Rigidbody has collided with a crashable object!");
            //Add crashing here
        }
    }
}
