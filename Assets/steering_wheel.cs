using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class steering_wheel : MonoBehaviour {
    public float speed = 50f;
    private float rotateBack = 0f;
    private float angleZero = -60f;
    private float angle = -60f;


    void Update() {
        angle += - Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        angle = Mathf.Clamp(angle, angleZero - 120, angleZero + 120);

        if (Input.GetAxis("Horizontal") == 0 && angle != angleZero) {
            angle += rotateBack * Time.deltaTime;
            if (angle > 0) {
                rotateBack = -speed;
                if (angle + rotateBack * Time.deltaTime < angleZero) {
                    angle = angleZero;
                }
            } else {
                rotateBack = speed;
                if (angle + rotateBack * Time.deltaTime > angleZero) {
                    angle = angleZero;
                }
            }
        }
        transform.rotation = Quaternion.Euler(angle, 96f, 73f);
    }
}
