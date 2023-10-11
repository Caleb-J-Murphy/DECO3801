using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class steering_wheel_2 : MonoBehaviour {
    public float rotationSpeed = 100f;
    private float totalRotation = 0f;
    private bool canTurn = false;

    void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            canTurn = !canTurn;
        }

        if (!canTurn) {
            return;
        }

        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        if (rotation != 0) {
            totalRotation += rotation;
            totalRotation = Mathf.Clamp(totalRotation, -120f, 120f);
        } else {
            if (totalRotation < 0) {
                totalRotation += 1.5f * rotationSpeed * Time.deltaTime;
                totalRotation = Mathf.Clamp(totalRotation, -120f, 0f);
            } else if (totalRotation > 0) {
                totalRotation -= 1.5f * rotationSpeed * Time.deltaTime;
                totalRotation = Mathf.Clamp(totalRotation, 0f, 120f);
            }
        }

        transform.localRotation = Quaternion.Euler(0, totalRotation, 0);
    }
}
