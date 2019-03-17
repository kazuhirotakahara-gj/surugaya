using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseOrderSpawner : MonoBehaviour
{
    public GameObject PreviewImages;
    public GameObject PurchaseOrderPrefab;
    public GameObject Spawned;

    [HideInInspector]
    public LevelController LevelController;
    
    private float WaitIntervalSecondMin = 0.5f;
    private float WaitIntervalSecondMax = 1.0f;
    private float WaitInterval = 0.0f;

    [HideInInspector]
    public PurchaseOrderScript lastPurchaseOrder = null;

	public PurchaseOrderScript.Order CurrentOrder;

	public PurchaseOrderScript.Order NextOrder;

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
        if (lastPurchaseOrder.IsBoxSet || lastPurchaseOrder.IsOutBoxed
			|| (lastPurchaseOrder.DisplayLimitTimer <= 0.0f && lastPurchaseOrder.IsOrdering)
			)
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

        var gos = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (var go in gos)
        {
            if (go != null && go.transform.parent == null)
            {
                var lc = go.GetComponent<LevelController>();
                if (lc)
                {
                    LevelController = lc;
                }
            }
        }

		NextOrder = MakeRandomOrder();
        Spawn();
    }

    PurchaseOrderScript.Order MakeRandomOrder()
    {
        var condidateItemNames = new List<PurchaseOrderScript.ItemName>();
        foreach (var item in LevelController.CondidateItems)
        {
            var itemImage = item.GetComponentInChildren<ItemImage>();
            condidateItemNames.Add(itemImage.Name);
        }

		uint createSum = (uint)Random.Range(1, Mathf.Max(1,CurrentLevel.OrderMax)+1);
        int[] createItem = new int[3] { (int)PurchaseOrderScript.ItemName.eITEM_INVALID, (int)PurchaseOrderScript.ItemName.eITEM_INVALID, (int)PurchaseOrderScript.ItemName.eITEM_INVALID, }; 

        for(int n = 0; n < createSum; n++)
        {
            createItem[n] = Random.Range(0, condidateItemNames.Count);
        }

		return new PurchaseOrderScript.Order((PurchaseOrderScript.ItemName)createItem[0], (PurchaseOrderScript.ItemName)createItem[1], (PurchaseOrderScript.ItemName)createItem[2]);
    }

    public void Spawn()
    {
		CurrentOrder = NextOrder;
		NextOrder = MakeRandomOrder();

        var go = Instantiate(PurchaseOrderPrefab, Spawned.transform) as GameObject;
        go.transform.localPosition = Vector3.zero;

        lastPurchaseOrder = go.GetComponentInChildren<PurchaseOrderScript>();
        lastPurchaseOrder.LevelController = LevelController;
		lastPurchaseOrder.SetProperty(CurrentOrder);
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
