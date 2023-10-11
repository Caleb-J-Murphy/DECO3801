using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotateSpeed = 50f;

    void Update()
    {
        if (Input.GetKey(KeyCode.W)) {
            transform.Rotate(Vector3.left, rotateSpeed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.S)) {
            transform.Rotate(Vector3.right, rotateSpeed * Time.deltaTime);
        }
    }
}
