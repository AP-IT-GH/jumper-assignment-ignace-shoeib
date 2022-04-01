using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject ObjectToSpawn;
    void Start()
    {
        StartCoroutine(SpawnObject());
    }

    IEnumerator SpawnObject()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(4f,6.5f));
            Spawn();
        }
    }
    void Spawn()
    {
        var spawnedObject = Instantiate(ObjectToSpawn);
        spawnedObject.transform.parent = transform.parent;
        spawnedObject.transform.localPosition = transform.localPosition;
    }
}