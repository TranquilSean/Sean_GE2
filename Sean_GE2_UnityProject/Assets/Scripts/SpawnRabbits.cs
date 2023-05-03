using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRabbits : MonoBehaviour
{
    // The prefab to spawn as a child object
    public GameObject prefab;
    public int maxCount = 5;
    public float spawnCheckDelay = 10f;
    public GameObject[] spawnpoints;
    

    private void Start()
    {
        if (transform.childCount < maxCount)
        {
            int i = Random.Range(0, spawnpoints.Length);
            Vector3 pos = spawnpoints[i].transform.position;
            GameObject objectSpawn = Instantiate(prefab, new Vector3(pos.x, -1, pos.z), Quaternion.identity, transform);
        }
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnCheckDelay);

            if (transform.childCount < maxCount)
            {
                int i = Random.Range(0, spawnpoints.Length);
                Vector3 pos = spawnpoints[i].transform.position;
                GameObject objectSpawn = Instantiate(prefab, new Vector3(pos.x, -1, pos.z), Quaternion.identity, transform);
            }
        }
    }
}