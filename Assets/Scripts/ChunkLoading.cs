using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChunkLoading : MonoBehaviour
{
    public Transform startPoint, endPoint;
    public Transform player;
    public GameObject chunk;
    public int count = 2;

    private List<GameObject> allChunks;
    private float prefabLength, nextZ;
    private GameObject prevChunk;

    void Start()
    {
        prefabLength = Mathf.Abs(endPoint.position.z - startPoint.position.z);
        prevChunk = gameObject;
        nextZ = prevChunk.transform.position.z  - prefabLength;
        allChunks = new List<GameObject>();
        
        //allChunks.Add(prevChunk);
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

        if (player != null &&

            allChunks.Count <= count &&
            Mathf.Abs(player.transform.position.z - prefabLength) <= Mathf.Abs(nextZ)) {
            prevChunk = Instantiate(chunk, new Vector3(prevChunk.transform.position.x, prevChunk.transform.position.y, nextZ), Quaternion.identity);
            allChunks.Add(prevChunk);
            nextZ -= prefabLength;
        }
    }
}