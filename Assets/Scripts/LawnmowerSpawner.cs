using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawnmowerSpawner : MonoBehaviour
{
    public GameObject lawnMowerPrefab;
    public float spawnDistance;
    public float spawnInterval;
    public GameObject spawnArea;
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            Spawn();
        }
    }

    void Spawn()
    {
        Vector3 moveDirection = Vector3.zero;
        Vector3 spawnPosition = Vector3.zero;

        int randomWallIndex = Random.Range(0, 4);
        Vector3 spawnAreaSize = spawnArea.GetComponent<BoxCollider>().size;


        float rotation = 0;

        switch (randomWallIndex)
        {
            case 0:
                spawnPosition = new Vector3(-spawnAreaSize.x / 2 + spawnDistance, 0, Random.Range(-spawnAreaSize.z / 2 + spawnDistance, spawnAreaSize.z / 2 - spawnDistance));
                moveDirection = Vector3.right;
                break;
            case 1:
                spawnPosition = new Vector3(spawnAreaSize.x / 2 - spawnDistance, 0, Random.Range(-spawnAreaSize.z / 2 + spawnDistance, spawnAreaSize.z / 2 - spawnDistance));
                moveDirection = Vector3.left;
                rotation = 90;
                break;
            case 2:
                spawnPosition = new Vector3(Random.Range(-spawnAreaSize.x / 2 + spawnDistance, spawnAreaSize.x / 2 - spawnDistance), 0, spawnAreaSize.z / 2 - spawnDistance);
                moveDirection = Vector3.back;
                rotation = 180;
                break;
            case 3: 
                spawnPosition = new Vector3(Random.Range(-spawnAreaSize.x / 2 + spawnDistance, spawnAreaSize.x / 2 - spawnDistance), 0, -spawnAreaSize.z / 2 + spawnDistance);
                moveDirection = Vector3.forward;
                rotation = 270;
                break;
        }

        spawnPosition = spawnArea.transform.TransformPoint(spawnPosition);
        
        GameObject lawnMower = Instantiate(lawnMowerPrefab, spawnPosition, Quaternion.identity);
        lawnMower.transform.parent = spawnArea.transform;

        try
        {
            StartCoroutine(DelayAndInitializeLawnMower(lawnMower, moveDirection, rotation));
        }
        catch(System.Exception ex)
        {
            Debug.Log(randomWallIndex);
            Debug.Log(spawnAreaSize);
            Debug.Log(spawnPosition);

        }
    }
    

    IEnumerator DelayAndInitializeLawnMower(GameObject lawnMower, Vector3 moveDirection, float rotation)
    {
        yield return null;

        Lawnmower lawnmower = lawnMower.GetComponent<Lawnmower>();
        lawnmower.ChangeMoveDirection(moveDirection);
        lawnmower.ChangeSpawnArea(spawnArea);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, spawnDistance);
    }
}
