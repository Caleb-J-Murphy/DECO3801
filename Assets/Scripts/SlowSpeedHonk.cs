using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlowSpeedHonk : MonoBehaviour
{
    public Rigidbody playerCar;

    private AudioSource audio;
    private float speedKPH;

    public void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void Update()
    {
        speedKPH = playerCar.velocity.magnitude * 3.6f;
        if (speedKPH < 20f && Vector3.Distance(playerCar.transform.position, transform.position) < 20) {
            if (!audio.isPlaying) {
                audio.Play();
            }
        }
    }
}