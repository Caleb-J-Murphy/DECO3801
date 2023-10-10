using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// attach to player car 
public class CarCollideAudio : MonoBehaviour
{
    public AudioSource slowCrash, fastCrash;
    private AudioSource audio;

    public void Start()
    {
        ;
    }

    public void Update()
    {
        ;
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
        } else if (collision.gameObject.tag == "building") {
            Rigidbody playerCar = GetComponent<Rigidbody>();
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
}