using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawnmowerSpawner : MonoBehaviour
{
    public GameObject lawnMowerPrefab;
    public float spawnDistance;
    public float spawnInterval;
    public GameObject spawnArea;
    private string wallTag = "Wall";
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
        GameObject[] walls = GameObject.FindGameObjectsWithTag(wallTag);
        GameObject randomWall = walls[Random.Range(0, walls.Length)];

        Vector3 spawnPosition = randomWall.transform.position + Random.insideUnitSphere * spawnDistance;
        spawnPosition = spawnArea.transform.TransformPoint(spawnPosition.x, 0, spawnPosition.z);

        GameObject lawnMower = Instantiate(lawnMowerPrefab, spawnPosition, Quaternion.identity);

        lawnMower.transform.parent = spawnArea.transform;

        StartCoroutine(DelayAndInitializeLawnMower(lawnMower, randomWall));
    }

    IEnumerator DelayAndInitializeLawnMower(GameObject lawnMower, GameObject randomWall)
    {
        yield return null;

        Lawnmower lawnmowerScript = lawnMower.GetComponent<Lawnmower>();
        float wallRotationY = randomWall.transform.rotation.eulerAngles.y;

        Vector3 moveDirection = new Vector3(Mathf.Sin(wallRotationY * Mathf.Deg2Rad), 0, Mathf.Cos(wallRotationY * Mathf.Deg2Rad));
        Vector3 rotatedMoveDirection = spawnArea.transform.TransformDirection(moveDirection);
        lawnmowerScript.ChangeMoveDirection(rotatedMoveDirection);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, spawnDistance);
    }
}
