using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseOrderScript : MonoBehaviour
{
    [HideInInspector]
    public LevelController LevelController;

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


    public enum PurchaseOrderStete
    {
        eState_Create = 0,      // 生成中動作
        eState_Wait,            // 待機中動作
        eState_Pick,            // 選択中動作
        eState_Set,             // 段ボールへのセット動作
        eState_Shipment,        // 出荷動作
        eState_Result,          // 結果動作
        eState_Delete,          // 削除動作
    };

    public class Order
    {
        public ItemName mItem1;
        public ItemName mItem2;
        public ItemName mItem3;

        public bool mIsSuccess = false;
        public Order(IEnumerable<ItemName> items)
        {
            var i = 0;
            foreach(var item in items)
            {
                switch(i++)
                {
                    case 0:
                        mItem1 = item;
                        break;
                    case 1:
                        mItem2 = item;
                        break;
                    case 2:
                        mItem3 = item;
                        break;
                }
            }
        }
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
                item3 == ItemName.eITEM_INVALID)
            {
                mIsSuccess = true;
                return true;
            }

            mIsSuccess = false;
            return false;
        }

        public bool IsSameCheck(ItemName item1, ItemName item2, ItemName item3)
        {
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
                item3 == ItemName.eITEM_INVALID)
            {
                mIsSuccess = true;
                return true;
            }

            mIsSuccess = false;
            return false;
        }
    }

    public GameObject Item1Object;
    public GameObject Item2Object;
    public GameObject Item3Object;

    private bool[] IsDispObject = new bool[] { false, false, false };
    private int[] IsDispSize = new int[] { 0, 0, 0 };
    private int[] IsDispIndex = new int[] { 0, 0, 0 };

    public int Rand1Parcent ;
    public int Rand2Parcent ;
    public int Rand3Parcent ;
    private Order mOrder;
    private int mWaitDestroyFlame = 0;

    private PurchaseOrderStete mState = PurchaseOrderStete.eState_Create;

    public static GameObject Create(GameObject _parent, string _path)
    {
        GameObject prefab = Resources.Load<GameObject>(_path);
        GameObject instance = GameObject.Instantiate(prefab);

        if (_parent != null)
        {
            instance.transform.SetParent(_parent.transform);
            var pos = instance.transform.position;
            pos.z = 100;
            instance.transform.position = pos;
        }
        return instance;
    }

    void SetProperty(Order order)
    {
        mOrder = order;
    }

    // Start is called before the first frame update
    void Start()
    {
        // ランダム生成。必要なければ消しちゃってください。
        RandCreateProperty();
        setImage();
        mState = PurchaseOrderStete.eState_Wait;
    }

    /// <summary>
    /// MouseのPickをキャンセル
    /// </summary>
    public bool TryMouseRelease()
    {
        if (mState != PurchaseOrderStete.eState_Pick)
            return false;

        mState = PurchaseOrderStete.eState_Wait;
        this.transform.localPosition = Vector3.zero;
        return true;
    }

    /// <summary>
    /// MouseでPickしたい
    /// </summary>
    public bool TryMousePick()
    {
        if (mState != PurchaseOrderStete.eState_Wait)
            return false;

        mState = PurchaseOrderStete.eState_Pick;
        return true;
    }

    public bool IsBoxSet
    {
        get { return mState == PurchaseOrderStete.eState_Set; }
    }

    public bool TryOutBox(ItemJunction junc)
    {
        if (junc?.ItemImages == null)
            return false;

        if (junc.ItemImages.transform.childCount != 0)
            return false;

        this.gameObject.transform.parent = junc.ItemImages.transform;
        mState = PurchaseOrderStete.eState_Set;
        GetComponent<BoxCollider2D>().enabled = false;

        var itemImages = gameObject.GetComponentsInChildren<ItemImage>();
        foreach(var itemImage in itemImages)
        {
            itemImage.GetComponent<PolygonCollider2D>().enabled = false;
        }

        return true;
    }

    void RandCreateProperty()
    {
        var condidateItemNames = new List<ItemName>();
        foreach (var item in LevelController.CondidateItems)
        {
            var itemImage = item.GetComponentInChildren<ItemImage>();
            condidateItemNames.Add(itemImage.Name);
        }

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
            createItem[n] = Random.Range(0, condidateItemNames.Count);
        }

        SetProperty( new Order((ItemName)createItem[0], (ItemName)createItem[1], (ItemName)createItem[2]));
    }

    void setImageByItem(ItemName itemName, GameObject targetObject)
    {
        if (itemName != ItemName.eITEM_INVALID)
        {
            targetObject.SetActive(true);

            foreach (var item in LevelController.CondidateItems)
            {
                var itemImage = item.GetComponentInChildren<ItemImage>();
                if (itemImage && itemImage.Name == itemName)
                {
                    targetObject.gameObject.GetComponentInChildren<ItemImage>().Name = itemName;
                    var render = itemImage.gameObject.GetComponentInChildren<SpriteRenderer>();
                    if (render)
                    {
                        targetObject.GetComponentInChildren<SpriteRenderer>().sprite = render.sprite;
                    }
                }
            }
        }
        else
        {
            targetObject.SetActive(false);
        }
    }

    void setImage()
    {
        setImageByItem(mOrder.mItem1, Item1Object);
        setImageByItem(mOrder.mItem2, Item2Object);
        setImageByItem(mOrder.mItem3, Item3Object);
    }

    // Update is called once per frame
    void Update()
    {
        switch(mState)
        {
            case PurchaseOrderStete.eState_Create:
                {
                    mState = PurchaseOrderStete.eState_Wait;
                }
                break;
            case PurchaseOrderStete.eState_Wait:
                {

                }
                break;
            case PurchaseOrderStete.eState_Pick:
                {

                }
                break;
            case PurchaseOrderStete.eState_Result:
                {
                    if(mOrder.mIsSuccess)
                    {
                        //成否表示
                    }
                    mWaitDestroyFlame = 60;
                    mState = PurchaseOrderStete.eState_Delete;
                }
                break;
            case PurchaseOrderStete.eState_Set:
                {

                }
                break;
            case PurchaseOrderStete.eState_Shipment:
                {

                }
                break;
            case PurchaseOrderStete.eState_Delete:
                {
                    mWaitDestroyFlame--;
                    if (mWaitDestroyFlame == 0)
                    {
                        Destroy(this.gameObject);
                    }
                }
                break;
            default:
                break;
        }
    }

    public bool CheckOrder(Order order)
    {
        mState = PurchaseOrderStete.eState_Result;
        return mOrder.IsSameCheck(order);
    }
    public bool CheckOrder(ItemName item1,ItemName item2 = ItemName.eITEM_INVALID, ItemName item3 = ItemName.eITEM_INVALID)
    {
        mState = PurchaseOrderStete.eState_Result;
        return mOrder.IsSameCheck(item1,item2,item3);
    }

    public bool CheckOrder(IEnumerable<ItemImage> items)
    {
        if (items == null)
            return false;

        var image1 = Item1Object?.GetComponentInChildren<ItemImage>();
        var image2 = Item2Object?.GetComponentInChildren<ItemImage>();
        var image3 = Item3Object?.GetComponentInChildren<ItemImage>();

        var enums = new List<ItemName>();
        if(image1 != null && Item1Object.activeInHierarchy)
            enums.Add(image1.Name);

        if (image2 != null && Item2Object.activeInHierarchy)
            enums.Add(image2.Name);

        if (image3 != null && Item3Object.activeInHierarchy)
            enums.Add(image3.Name);

        Dictionary<ItemName, int> srcDict = new Dictionary<ItemName, int>();
        Dictionary<ItemName, int> dstDict = new Dictionary<ItemName, int>();

        foreach(var e in enums)
        {
            var value = 0;
            if (srcDict.TryGetValue(e, out value))
                srcDict[e] = ++value;
            else
                srcDict[e] = 1;
        }

        foreach (var item in items)
        {
            var e = item.Name;
            var value = 0;
            if (dstDict.TryGetValue(e, out value))
                dstDict[e] = ++value;
            else
                dstDict[e] = 1;
        }

        if(srcDict.Keys.Count != dstDict.Keys.Count)
            return false;

        foreach(var k in srcDict.Keys)
        {
            if (dstDict.ContainsKey(k) == false)
                return false;

            if (dstDict[k] != srcDict[k])
                return false;
        }

        return true;
    }


}
