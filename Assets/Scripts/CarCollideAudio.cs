using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Attach this to the main car
 * the main car should have 2 audios
 * (1) for a crash at slow speeds <= 40 km/h
 * (2) for a crash at high speeds > 40 km/h
 */
public class CarCollideAudio : MonoBehaviour
{
    public AudioSource slowCrash, fastCrash;
    private AudioSource audio;

    private Rigidbody playerCar;

    public void Start()
    {
        playerCar = GetComponent<Rigidbody>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "NPC_person") {
            audio = collision.gameObject.GetComponent<AudioSource>();
            if (audio) {
                if (!audio.isPlaying) {
                    audio.Play();
                }
            }
        }
        if ((playerCar.velocity.magnitude * 3.6f) > 40) {
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