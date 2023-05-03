using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    // The prefab to spawn as a child object
    public GameObject prefab;
    public int maxCount = 5;
    float min = -20f;
    float max = 20f;

    public float spawnCheckDelay = 10f;

    private void Start()
    {
        if (transform.childCount < 15)
        {
            float randomX = Random.Range(min, max);
            float randomZ = Random.Range(min, max);
            GameObject foodObject = Instantiate(prefab, new Vector3(randomX, -1, randomZ), Quaternion.identity, transform);
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
                float randomX = Random.Range(min, max);
                float randomZ = Random.Range(min, max);
                GameObject objectSpawn = Instantiate(prefab, new Vector3(randomX, -1, randomZ), Quaternion.identity, transform);
            }
        }
    }
}