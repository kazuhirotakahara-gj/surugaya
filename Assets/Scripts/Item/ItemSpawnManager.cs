using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    public float IntervalSecond = 2.0f;

    public float CurrentIntervalSecond = 0.0f;

    public float CurrentMoveSpeed = 1.0f;

    public Vector3 SpawnOffset = new Vector3(-10, 1.2f, 0);

	private LevelController _LevelController;

	public PurchaseOrderSpawner[] PurchaseOrderSpawners;

	public GameObject[] AllItems;

	public float AnotherSpawnProbability = 33f;
	public float NextSpawnProbability = 33f;

	private void Awake()
	{
        var gos = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (var go in gos)
        {
            if (go != null && go.transform.parent == null)
            {
                var lc = go.GetComponent<LevelController>();
                if (lc)
                {
                    _LevelController = lc;
                }
            }
        }
	}

	void Start()
    {
		AnotherSpawnProbability = CurrentLevel.AnotherSpawnProbability;
		NextSpawnProbability = CurrentLevel.NextSpawnProbability;
    }

    void Update()
    {
    }

    void OnFlowRequest()
    {
        SpawnRandom();
    }

	public List<PurchaseOrderScript.ItemName> lotteryItemNames = new List<PurchaseOrderScript.ItemName>(3/*Spawners*/ * 3/*MaxOrders*/);

    GameObject SpawnRandom()
    {
		lotteryItemNames.Clear();
		foreach (var spawner in PurchaseOrderSpawners)
		{
			{
				var prob = Mathf.Max(0, 100 - (AnotherSpawnProbability + NextSpawnProbability));
				var spawnRate = (int)(prob * (spawner.lastPurchaseOrder.DisplayLimitRatio < 0.3f ? 0.7f : 1.0f));
				for (int i = 0; i < spawnRate; ++i)
				{
					if (spawner.CurrentOrder.mItem1 != PurchaseOrderScript.ItemName.eITEM_INVALID) lotteryItemNames.Add(spawner.CurrentOrder.mItem1);
					if (spawner.CurrentOrder.mItem2 != PurchaseOrderScript.ItemName.eITEM_INVALID) lotteryItemNames.Add(spawner.CurrentOrder.mItem2);
					if (spawner.CurrentOrder.mItem3 != PurchaseOrderScript.ItemName.eITEM_INVALID) lotteryItemNames.Add(spawner.CurrentOrder.mItem3);
				}
			}

			{
				var prob = AnotherSpawnProbability + NextSpawnProbability;
				var spawnRate = (int)(prob * (spawner.lastPurchaseOrder.DisplayLimitRatio < 0.3f ? 1.0f : 0.5f));
				for (int i = 0; i < spawnRate; ++i)
				{
					if (spawner.NextOrder.mItem1 != PurchaseOrderScript.ItemName.eITEM_INVALID) lotteryItemNames.Add(spawner.NextOrder.mItem1);
					if (spawner.NextOrder.mItem2 != PurchaseOrderScript.ItemName.eITEM_INVALID) lotteryItemNames.Add(spawner.NextOrder.mItem2);
					if (spawner.NextOrder.mItem3 != PurchaseOrderScript.ItemName.eITEM_INVALID) lotteryItemNames.Add(spawner.NextOrder.mItem3);
				}
			}
		}

		var allItems = CurrentLevel.AllItems == null ? AllItems : CurrentLevel.AllItems;
		for (int i = 0; i < (int)AnotherSpawnProbability; ++i)
		{
			for (int k = 0; k < (int)PurchaseOrderScript.ItemName.eITEM_INVALID; ++k)
			{
				lotteryItemNames.Add((PurchaseOrderScript.ItemName)k);
			}
		}

		var seed = Random.Range(0, lotteryItemNames.Count);
		GameObject source = AllItems[(int)lotteryItemNames[seed]];

        if (source == null)
            return null;

        var created     = GameObject.Instantiate(source, SpawnOffset, Quaternion.identity);
        var createdItem = created.GetComponent<Item>();

        createdItem.AutoMove = true;
		createdItem._LevelController = _LevelController;
        return null;
    }
}
