using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardboardSpawner : MonoBehaviour
{
    public GameObject PreviewImages;
    public GameObject CardboardPrefab;
    public GameObject Spawned;

    private float WaitIntervalSecondMin = 0.5f;
    private float WaitIntervalSecondMax = 1.0f;
    private float WaitInterval = 0.0f;

    private Cardboard lastCardboard = null;

    void ValidateWaitInterval()
    {
        if (WaitIntervalSecondMin >= WaitIntervalSecondMax)
        {
            throw new System.Exception("Invalid interval.");
        }

        ResetWaitInterval();
    }

    void ResetWaitInterval()
    {
        WaitInterval = Random.Range(WaitIntervalSecondMin, WaitIntervalSecondMax);
    }

    void UpdateWaitInterval()
    {
        if (lastCardboard.IsScreenOver)
        {
            WaitInterval -= Time.deltaTime;
            if (WaitInterval <= 0.0f)
            {
                Spawn();
                ResetWaitInterval();
            }
        }
    }

    private void Initialize()
    {
        PreviewImages.SetActive(false);

        Spawn();
    }

    public void Spawn()
    {
        var go = Instantiate(CardboardPrefab, Spawned.transform) as GameObject;
        go.transform.localPosition = Vector3.zero;

        lastCardboard = go.GetComponent<Cardboard>();
    }

    void OnSetFillCardboardParameter(float[] fillParams)
    {
        WaitIntervalSecondMin = fillParams[0];
        WaitIntervalSecondMax = fillParams[1];
    }

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        UpdateWaitInterval();
    }
}
