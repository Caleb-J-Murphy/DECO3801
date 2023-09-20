using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChunkLoading : MonoBehaviour
{
    public Transform startPoint, endPoint;
    public Transform cameraTransform;
    public GameObject prefabRef;

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
            if (chunk.transform.position.z - prefabLength > cameraTransform.transform.position.z) {
                tmpChunk = chunk;                
                Destroy(chunk);
            }
        }
        
        if (tmpChunk != null) {
            allChunks.Remove(tmpChunk);    
        }

        if (cameraTransform != null &&
            allChunks.Count <= 3 &&
            Mathf.Abs(cameraTransform.transform.position.z - prefabLength) <= Mathf.Abs(nextZ)) {
            prevChunk = Instantiate(prefabRef, new Vector3(prevChunk.transform.position.x, prevChunk.transform.position.y, nextZ), Quaternion.identity);
            allChunks.Add(prevChunk);
            nextZ -= prefabLength;
        }
    }
}