using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetectorController : MonoBehaviour
{
    public bool ObstacleDetected = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + "Object entered: " + gameObject.name);
        ObstacleDetected = true;
    }

    void OnTriggerExit(Collider other) {
        Debug.Log(other.gameObject.name + "Object exited: " + gameObject.name);
        ObstacleDetected = false;
    }
}
