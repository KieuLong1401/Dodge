using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hipnogumelo : MonoBehaviour
{
    public GameObject hipnogumeloPrefab;
    public float spawnInterval = 4f;
    public Vector3 mapBounds = new Vector3(10f, 1f, 10f);
    public float spawnRadius = 1f;
    public float lifetime = 3f;

    private GameObject currentItem;

    void Start()
    {
        StartCoroutine(SpawnItemRoutine());
    }

    IEnumerator SpawnItemRoutine() {
        while (true) {
            yield return new WaitForSeconds(spawnInterval);

            SpawnItem();
        }
    }

    void SpawnItem() {
        if (currentItem != null) return;

        Vector3 spawnPosition;
        int attempts = 0;
        do {
            spawnPosition = new Vector3(
                Random.Range(-mapBounds.x, mapBounds.x),
                mapBounds.y,
                Random.Range(-mapBounds.z, mapBounds.z)
            );
            attempts++;
        }
        while (Physics.CheckSphere(spawnPosition, spawnRadius) && attempts < 100);

        if (attempts < 100) {
            currentItem = Instantiate(hipnogumeloPrefab, spawnPosition, Quaternion.identity);
            StartCoroutine(ItemLifetimeRoutine(currentItem));
        }
    }

    IEnumerator ItemLifetimeRoutine(GameObject item) {
        yield return new WaitForSeconds(lifetime);

        if (item != null) {
            Destroy(item);
        }
    }

    void Update()
    {
        
    }
}