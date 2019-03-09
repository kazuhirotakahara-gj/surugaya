using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseOederScript : MonoBehaviour
{
    public enum ItemName{
        eITEM_0 = 0,
        eITEM_1,
        eITEM_2,
        eITEM_3,
        eITEM_4,
        eITEM_5,
        eITEM_6,
        eITEM_7,
        eITEM_8,
        eITEM_9,
        eITEM_A,
        eITEM_B,
        eITEM_INVALID,
        eITEM_MAX,
    };

    List<int> OrderItemList = new List<int>((int)ItemName.eITEM_MAX)
    {
        0,0,0,0,0,0,0,0,0,0,0,0
    };

    public static GameObject Create(GameObject _parent, string _path)
    {
        GameObject prefab = Resources.Load<GameObject>(_path);
        GameObject instance = GameObject.Instantiate(prefab);]

        if (_parent != null)
        {
            instance.transform.SetParent(_parent.transform);
        }
        return instance;
    }


    void SetProperty(ItemName item1,ItemName item2 = ItemName.eITEM_INVALID, ItemName item3 = ItemName.eITEM_INVALID)
    {
        OrderItemList[(int)item1] += 1;
        OrderItemList[(int)item2] += 1;
        OrderItemList[(int)item3] += 1;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool ItemsetCheck(ItemName item1, ItemName item2 = ItemName.eITEM_INVALID, ItemName item3 = ItemName.eITEM_INVALID)
    {
        OrderItemList[(int)item1] -= 1;
        OrderItemList[(int)item2] -= 1;
        OrderItemList[(int)item3] -= 1;
        foreach(var item in OrderItemList)
        {
            if(item != 0)
            {
                return false;
            }
        }
        return true;
    }
}
