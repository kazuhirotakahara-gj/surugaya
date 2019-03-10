using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    #region FlowIntervals
    public float FlowIntervalSecondMin = 0.5f;
    public float FlowIntervalSecondMax = 1.0f;
    private float FlowInterval = 0.0f;

    void ValidateFlowInterval()
    {
        if (FlowIntervalSecondMin >= FlowIntervalSecondMax)
        {
            throw new System.Exception("Invalid interval.");
        }

        ResetFlowInterval();
    }

    void ResetFlowInterval()
    {
        FlowInterval = Random.Range(FlowIntervalSecondMin, FlowIntervalSecondMax);
    }

    void UpdateFlowInterval()
    {
        FlowInterval -= Time.deltaTime;
        if (FlowInterval <= 0.0f)
        {
            BroadcastFlowRequestMessage();
            ResetFlowInterval();
        }
    }

    void BroadcastFlowRequestMessage()
    {
        var gos = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (var go in gos)
        {
            if (go != null && go.transform.parent == null)
            {
                go.BroadcastMessage("OnFlowRequest", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
    #endregion

    #region FillCardboardIntervals
    public float FillCardboardIntervalSecondMin = 1.0f;
    public float FillCardboardIntervalSecondMax = 2.0f;

    void BroadcastFillCardboardParameter()
    {
        var gos = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (var go in gos)
        {
            if (go != null && go.transform.parent == null)
            {
                float[] storage = new float[2];
                storage[0] = FillCardboardIntervalSecondMin;
                storage[1] = FillCardboardIntervalSecondMax;
                go.BroadcastMessage("OnSetFillCardboardParameter", storage, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
    #endregion

    #region FillPurchaseOrderIntervals
    public float FillPurchaseOrderIntervalSecondMin = 1.0f;
    public float FillPurchaseOrderIntervalSecondMax = 2.0f;

    void BroadcastFillPurchaseOrderParameter()
    {
        var gos = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (var go in gos)
        {
            if (go != null && go.transform.parent == null)
            {
                float[] storage = new float[2];
                storage[0] = FillPurchaseOrderIntervalSecondMin;
                storage[1] = FillPurchaseOrderIntervalSecondMax;
                go.BroadcastMessage("OnSetFillPurchaseOrderParameter", storage, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
    #endregion

    #region ItemSpawn
    public GameObject[] CondidateItems = new GameObject[] { };

    void BroadcastCondidateItems()
    {
        var gos = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (var go in gos)
        {
            if (go != null && go.transform.parent == null)
            {
                go.BroadcastMessage("OnSetCondidateItems", CondidateItems, SendMessageOptions.DontRequireReceiver);

                var poSpawner = go.GetComponent<PurchaseOrderSpawner>();
                if (poSpawner)
                {
                    poSpawner.LevelController = this;
                }
            }
        }
    }
    #endregion

    void Start()
    {
        ValidateFlowInterval();
        BroadcastFillCardboardParameter();
        BroadcastFillPurchaseOrderParameter();
        BroadcastCondidateItems();
    }

    void Update()
    {
        UpdateFlowInterval();
    }
}
