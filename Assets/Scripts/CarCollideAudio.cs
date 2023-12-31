using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Attach this to the main car
 * the main car should have 2 audios
 * (1) for a crash at slow speeds <= speedThreshold km/h
 * (2) for a crash at high speeds > speedThreshold km/h
 *
 * The collision can only occur once, even if the car (after a crash)
 * rolls into a building for example.
 */
public class CarCollideAudio : MonoBehaviour
{
    public AudioSource slowCrash, fastCrash, engineIdle, engineStart, earsRinging;
    public float speedThreshold = 40f;
    public float maxSpeedKPH = 100f;

    private AudioSource audio;
    private Rigidbody playerCar;
    private bool collision_state;
    private bool engineIdle_state;
    private float currSpeed;

    public void Start()
    {
        playerCar = GetComponent<Rigidbody>();
        collision_state = false;
        engineIdle_state = false;
    }

    float unity_to_kph(Rigidbody car)
    {
        return Mathf.FloorToInt(car.velocity.magnitude * 3.6f);
    }

    public void Update()
    {
        currSpeed = unity_to_kph(playerCar);
        float speedRatio = currSpeed/maxSpeedKPH;
        engineIdle.pitch = speedRatio + 1f;
        engineStart.pitch = speedRatio + 1f;
        if (engineIdle_state == false && !engineStart.isPlaying) {
            engineIdle.Play(); // plays on loop
            engineIdle_state = true;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision_state == true) {
            return;
        }
        collision_state = true;

        /* NPC scream */
        for (Transform collider = collision.transform; collider != null; collider = collider.parent) {
            if (collider.CompareTag("npc") && (audio = collider.GetComponent<AudioSource>()) != null) {
                if (!audio.isPlaying) {
                    audio.Play();
                    break;
                }
            }
        }

        /* car engine sound off */
        if (engineIdle.isPlaying) {
            engineIdle.Stop();
        }

        /* crash sound based on speed */
        if (currSpeed > speedThreshold) {
            if (!fastCrash.isPlaying) {
                fastCrash.Play();
            }

            if (!earsRinging.isPlaying) {
                earsRinging.Play();
            }
        } else {
            if (!slowCrash.isPlaying) {
                slowCrash.Play();
            }
        }
    }
}
