using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LoadXChunks : MonoBehaviour
{
    public Transform startPoint, endPoint;
    [SerializeField]
    public Transform player;

    [Tooltip("Excluding starting chunk")]
    public int count = 2;
    
    [Header("Chunks")]
    public GameObject chunk;
    public GameObject finalChunk;

    private List<GameObject> allChunks;
    private float prefabLength, nextZ;
    private GameObject prevChunk;
    private Vector3 pos = new Vector3(-0.360000014f,-0.100018501f,0f);

    void Start()
    {
        prefabLength = Mathf.Abs(endPoint.position.z - startPoint.position.z);
        prevChunk = gameObject;
        nextZ = prevChunk.transform.position.z  - prefabLength;
        allChunks = new List<GameObject>();
        
        //allChunks.Add(prevChunk);
        for (int leftToSpawn = count; leftToSpawn > 0; leftToSpawn--) {
            prevChunk = Instantiate(chunk, new Vector3(prevChunk.transform.position.x, prevChunk.transform.position.y, nextZ), Quaternion.identity);
            allChunks.Add(prevChunk);
            nextZ -= prefabLength;
        }

        // instantiate final chunk
        Instantiate(finalChunk, new Vector3(pos.x, pos.y, nextZ + 218), Quaternion.identity);
    }

    void Update()
    {
        GameObject tmpChunk = null;
        foreach (GameObject chunk in allChunks) {
            if (chunk.transform.position.z - prefabLength > player.transform.position.z) {
                tmpChunk = chunk;                
                Destroy(chunk);
            }
        }
        
        if (tmpChunk != null) {
            allChunks.Remove(tmpChunk);    
        }
    }
}