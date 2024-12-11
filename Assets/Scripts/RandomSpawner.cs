using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject objectPrefab;

    public float spawnInterval;
    public float spawnRadius;
    public GameObject spawnArea;
    public LayerMask obstacleLayer;
    private Vector3 spawnAreaSize;

    public float lifetime = 0;

    public float yPos;

    void Start()
    {
        spawnAreaSize = spawnArea.GetComponent<BoxCollider>().size;
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
    IEnumerator ObjectLifetimeRoutine(GameObject item) {
        yield return new WaitForSeconds(lifetime);

        if (item != null) {
            Destroy(item);
        }
    }

    void Spawn()
    {
        Vector3 spawnPosition;
        int attempts = 0;
        do {
            spawnPosition = new Vector3(
                Random.Range(-spawnAreaSize.x / 2 + spawnRadius, spawnAreaSize.x / 2 - spawnRadius),
                yPos,
                Random.Range(-spawnAreaSize.z / 2 + spawnRadius, spawnAreaSize.z / 2 - spawnRadius)
            );
            spawnPosition = spawnArea.transform.TransformPoint(spawnPosition);
            attempts++;
        }
        while (Physics.OverlapSphere(transform.position, spawnRadius, obstacleLayer).Length > 0 && attempts < 100);

        if (attempts < 100) {
            GameObject gameObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
            gameObject.transform.parent = spawnArea.transform;
            if(lifetime > 0)
            {
                StartCoroutine(ObjectLifetimeRoutine(gameObject));
            }
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, spawnRadius);
    }
}
