using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject prefab;
    public int ammount;
    public float timeInterval;

    private void Start()
    {
        Invoke("SpawnPrefab", timeInterval);
    }

    void SpawnPrefab()
    {
        //Stop spawning after all prefabs where spawned
        if (ammount <= 0)
            return;

        GameObject.Instantiate(prefab, transform.position, Quaternion.identity);
        Invoke("SpawnPrefab", timeInterval);
        ammount--;
    }
}
