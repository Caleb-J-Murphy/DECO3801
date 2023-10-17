using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlowSpeedHonk : MonoBehaviour
{
    public GameObject player;
    [Tooltip("If car goes below this speed (km/h), honk")]
    public float     speedThreshold = 10f;

    [Tooltip("The car needs <= this value (unity units) to honk")]
    public float     distanceToCar = 20f;

    private AudioSource audio;
    private float speedKPH;
    private Rigidbody playerCar;
    private float laneWidth = 7f;

    public void Start()
    {
        audio = GetComponent<AudioSource>();
        playerCar = player.GetComponent<Rigidbody>();
    }

    public void Update()
    {
        Vector3 npcGlobal = transform.TransformDirection(Vector3.forward); // transform to global direction?
        Vector3 playerGlobal = playerCar.transform.TransformDirection(Vector3.forward);
        float dotProduct = Vector3.Dot(npcGlobal, playerGlobal);

        if (dotProduct <= 1 && dotProduct > 0 && // facing the same direction (within 180 deg.)
            transform.position.z > player.transform.position.z && // player car in front of NPC car
            Mathf.Abs(transform.position.x - player.transform.position.x) < laneWidth) { // the car is in the same lane
            
            speedKPH = playerCar.velocity.magnitude * 3.6f;
            if (speedKPH < speedThreshold &&
                Vector3.Distance(playerCar.transform.position, transform.position) < distanceToCar) {
                if (!audio.isPlaying) {
                    audio.Play();
                }
            }
        }
    }
}