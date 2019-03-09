using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardboardSpawner : MonoBehaviour
{
    public GameObject PreviewImages;
    public GameObject CardboardPrefab;
    public GameObject Spawned;

    private Cardboard lastCardboard = null;

    public float NextSpawnWaitTime = 1.0f;
    private float NextSpawnWaitTimer = 0.0f;

    private void Initialize()
    {
        PreviewImages.SetActive(false);

        Spawn();
        NextSpawnWaitTimer = NextSpawnWaitTime;
    }

    public void Spawn()
    {
        var go = Instantiate(CardboardPrefab, Spawned.transform) as GameObject;
        go.transform.localPosition = Vector3.zero;

        lastCardboard = go.GetComponent<Cardboard>();
    }

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        if(lastCardboard.IsScreenOver)
        {
            NextSpawnWaitTimer -= Time.deltaTime;
            if (NextSpawnWaitTimer <= 0)
            {
                NextSpawnWaitTimer = NextSpawnWaitTime;
                Spawn();
            }
        }
    }
}
