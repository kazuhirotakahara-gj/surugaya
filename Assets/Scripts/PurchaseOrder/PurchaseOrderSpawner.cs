using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseOrderSpawner : MonoBehaviour
{
    public GameObject PreviewImages;
    public GameObject PurchaseOrderPrefab;
    public GameObject Spawned;

    private float WaitIntervalSecondMin = 0.5f;
    private float WaitIntervalSecondMax = 1.0f;
    private float WaitInterval = 0.0f;

    private PurchaseOrderScript lastPurchaseOrder = null;

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
        if (lastPurchaseOrder.IsBoxSet)
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
        var go = Instantiate(PurchaseOrderPrefab, Spawned.transform) as GameObject;
        go.transform.localPosition = Vector3.zero;

        lastPurchaseOrder = go.GetComponentInChildren<PurchaseOrderScript>();
    }

    void OnSetFillPurchaseOrderParameter(float[] fillParams)
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
