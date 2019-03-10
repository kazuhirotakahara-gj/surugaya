using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
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

    void Start()
    {
        ValidateFlowInterval();
    }

    void Update()
    {
        UpdateFlowInterval();
    }
}
