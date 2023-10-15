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
    public AudioSource slowCrash, fastCrash;
    public float speedThreshold = 40f;
    
    private AudioSource audio;
    private Rigidbody playerCar;
    private bool collision_state;

    public void Start()
    {
        playerCar = GetComponent<Rigidbody>();
        collision_state = false;
    }

    float unity_to_kph(Rigidbody car)
    {
        return (car.velocity.magnitude * 3.6f);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision_state == true) {
            return;
        }
        collision_state = true;

        for (Transform collider = collision.transform; ; collider = collider.parent) {
            if (collider.CompareTag("npc") && (audio = collider.GetComponent<AudioSource>()) != null) {
                if (!audio.isPlaying) {
                    audio.Play();
                    break;
                }
            }
            
        }

        if (unity_to_kph(playerCar) > speedThreshold) {
            if (!fastCrash.isPlaying) {
                fastCrash.Play();
            }
        } else {
            if (!slowCrash.isPlaying) {
                slowCrash.Play();
            }
        }
    }
}