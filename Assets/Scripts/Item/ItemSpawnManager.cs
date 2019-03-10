using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    private GameObject[] _ItemsList;

    public GameObject[] ItemsList
    {
        get { return _ItemsList; }
    }

    public float IntervalSecond = 2.0f;

    public float CurrentIntervalSecond = 0.0f;

    public float CurrentMoveSpeed = 1.0f;

    public Vector3 SpawnOffset = new Vector3(-10, 1.2f, 0);

    void OnSetCondidateItems(GameObject[] CondidateItems)
    {
        _ItemsList = CondidateItems;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
#if false
        if(CurrentIntervalSecond < 0)
        {
            CurrentIntervalSecond = IntervalSecond;
            SpawnRandom();
        }
        else
        {
            CurrentIntervalSecond -= Time.deltaTime;
        }
#endif
    }

    void OnFlowRequest()
    {
        SpawnRandom();
    }

    GameObject SpawnRandom()
    {
        var randIndex = Random.Range(0, _ItemsList.Length );

        var itemSrc = _ItemsList[randIndex];
        if (itemSrc == null)
            return null;

        var created     = GameObject.Instantiate(itemSrc, SpawnOffset, Quaternion.identity);
        var createdItem = created.GetComponent<Item>();

        createdItem.Speed = CurrentMoveSpeed;
        createdItem.AutoMove = true;
        return null;
    }
}
