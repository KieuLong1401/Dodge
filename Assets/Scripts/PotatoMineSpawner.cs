using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoMineSpawner : MonoBehaviour
{
    public GameObject PotatoMinePrefab;
    public Vector3 spawnAreaSize = new Vector3(10f, 1f, 10f);
    public float spawnRadius = 1f;
    public float spawnInterval = 5f;

    void Start()
    {
        StartCoroutine(SpawnPotatoMine());    
    }

    IEnumerator SpawnPotatoMine()
    {
        while (true)
        {
            Vector3 spawnPosition;
            int attempts = 0;
            do {
                spawnPosition = new Vector3(
                    Random.Range(-spawnAreaSize.x, spawnAreaSize.x),
                    spawnAreaSize.y,
                    Random.Range(-spawnAreaSize.z, spawnAreaSize.z)
                );
                attempts++;
            }
            while (Physics.CheckSphere(spawnPosition, spawnRadius) && attempts < 100);

            if (attempts < 100) {
                Instantiate(PotatoMinePrefab, spawnPosition, Quaternion.identity);
            }


            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void Update()
    {
        
    }
}
