using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseOrderScript : MonoBehaviour
{
    public enum ItemName
    {
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
        eITEM_INVALID,
        eITEM_MAX,
    };


    public class Order
    {
        public ItemName mItem1;
        public ItemName mItem2;
        public ItemName mItem3;

        public Order(ItemName item1,ItemName item2,ItemName item3)
        {
            mItem1 = item1;
            mItem2 = item2;
            mItem3 = item3;
        }

        public bool IsSameCheck(Order item)
        {
            ItemName item1 = item.mItem1;
            ItemName item2 = item.mItem2;
            ItemName item3 = item.mItem3;

            if (mItem1 == item1)
            {
                item1 = ItemName.eITEM_INVALID;
            }
            if (mItem1 == item2)
            {
                item2 = ItemName.eITEM_INVALID;
            }
            if (mItem1 == item3)
            {
                item3 = ItemName.eITEM_INVALID;
            }
            if (mItem2 == item1)
            {
                item1 = ItemName.eITEM_INVALID;
            }
            if (mItem2 == item2)
            {
                item2 = ItemName.eITEM_INVALID;
            }
            if (mItem2 == item3)
            {
                item3 = ItemName.eITEM_INVALID;
            }
            if (mItem3 == item1)
            {
                item1 = ItemName.eITEM_INVALID;
            }
            if (mItem3 == item2)
            {
                item2 = ItemName.eITEM_INVALID;
            }
            if (mItem3 == item3)
            {
                item3 = ItemName.eITEM_INVALID;
            }

            if (item1 == ItemName.eITEM_INVALID &&
                item2 == ItemName.eITEM_INVALID &&
                item3 == ItemName.eITEM_INVALID )
            {
                return true;
            }

            return false;
        }
    }

    public GameObject Item1Object;
    public GameObject Item2Object;
    public GameObject Item3Object;

    private bool[] IsDispObject = new bool[] { false, false, false };
    private int[] IsDispSize = new int[] { 0, 0, 0 };
    private int[] IsDispIndex = new int[] { 0, 0, 0 };

    public Image[] TexuteImage = new Image[11];

    private bool mIsDestory = false;

    public int Rand1Parcent ;
    public int Rand2Parcent ;
    public int Rand3Parcent ;
    private Order mOrder;
    private int mWaitDestroyFlame = 0;

    public static GameObject Create(GameObject _parent, string _path)
    {
        GameObject prefab = Resources.Load<GameObject>(_path);
        GameObject instance = GameObject.Instantiate(prefab);

        if (_parent != null)
        {
            instance.transform.SetParent(_parent.transform);
        }
        return instance;
    }


    void SetProperty(Order order)
    {
        mOrder = order;
    }

    public bool CheckOrder(Order order)
    {
        return mOrder.IsSameCheck(order);
    }
    // Start is called before the first frame update
    void Start()
    {
        // ランダム生成。必要なければ消しちゃってください。
        RandCreateProperty();
        setImage();
    }

    void RandCreateProperty()
    {
        var rand = Random.Range(0, Rand1Parcent+ Rand2Parcent+ Rand3Parcent);
        uint createSum = 0;
        int[] createItem = new int[3] { (int)ItemName.eITEM_INVALID, (int)ItemName.eITEM_INVALID, (int)ItemName.eITEM_INVALID, }; 
        if(rand < Rand1Parcent)
        {
            createSum = 1;
        }
        else if(rand < Rand1Parcent+ Rand2Parcent)
        {
            createSum = 2;
        }
        else
        {
            createSum = 3;
        }

        for(int n = 0; n < createSum; n++)
        {
            createItem[n] = Random.Range(0, (int)ItemName.eITEM_INVALID);
        }

        SetProperty( new Order((ItemName)createItem[0], (ItemName)createItem[1], (ItemName)createItem[2]));
    }

    void setImage()
    {
        if(mOrder.mItem1 != ItemName.eITEM_INVALID)
        {
            Item1Object.SetActive(true);
            Item1Object.GetComponent<Image>().sprite = TexuteImage[(int)mOrder.mItem1].sprite;
        }
        else
        {
            Item1Object.SetActive(false);

        }
        if (mOrder.mItem2 != ItemName.eITEM_INVALID)
        {
            Item2Object.SetActive(true);
            Item2Object.GetComponent<Image>().sprite = TexuteImage[(int)mOrder.mItem2].sprite;
        }
        else
        {
            Item2Object.SetActive(false);
        }
        if (mOrder.mItem3 != ItemName.eITEM_INVALID)
        {
            Item3Object.SetActive(true);
            Item3Object.GetComponent<Image>().sprite = TexuteImage[(int)mOrder.mItem3].sprite;
        }
        else
        {
            Item3Object.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(mIsDestory)
        {
            mWaitDestroyFlame--;
            if (mWaitDestroyFlame == 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

}
