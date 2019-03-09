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

    public GameObject Item1Object;
    public GameObject Item2Object;
    public GameObject Item3Object;
    public Canvas TextCanvas;

    private bool[] IsDispObject = new bool[] { false, false, false };
    private int[] IsDispSize = new int[] { 0, 0, 0 };
    private int[] IsDispIndex = new int[] { 0, 0, 0 };

    public Image[] TexuteImage = new Image[11];

    private bool mIsDestory = false;

    private int mWaitDestroyFlame = 0;
    List<int> OrderItemList = new List<int>((int)ItemName.eITEM_MAX)
    {
        0,0,0,0,0,0,0,0,0,0,0,0
    };

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


    void SetProperty(ItemName item1, ItemName item2 = ItemName.eITEM_INVALID, ItemName item3 = ItemName.eITEM_INVALID)
    {
        OrderItemList[(int)item1] += 1;
        OrderItemList[(int)item2] += 1;
        OrderItemList[(int)item3] += 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        //SetProperty(ItemName.eITEM_0, ItemName.eITEM_1, ItemName.eITEM_2);
        setImage();
    }

    void setImage()
    {
        int TextureIndex = 0;
        for (int n = 0; n < (int)ItemName.eITEM_INVALID; n++)
        {
            if (OrderItemList[n] > 0)
            {
                //アイテムの設定
                IsDispObject[TextureIndex] = true;
                IsDispSize[TextureIndex] = OrderItemList[n];
                IsDispIndex[TextureIndex] = n;
                TextureIndex += 1;
            }
        }
        for (int n = 0; n < 3; n++)
        {
            if (IsDispObject[n])
            {
                switch (n)
                {
                    case 0:
                        {
                            Item1Object.SetActive(true);
                            Item1Object.GetComponent<Image>().sprite = TexuteImage[IsDispIndex[n]].sprite;
                        }
                        break;
                    case 1:
                        {
                            Item2Object.SetActive(true);
                            Item2Object.GetComponent<Image>().sprite = TexuteImage[IsDispIndex[n]].sprite;
                        }
                        break;
                    case 2:
                        {
                            Item3Object.SetActive(true);
                            Item3Object.GetComponent<Image>().sprite = TexuteImage[IsDispIndex[n]].sprite;
                        }
                        break;
                }
            }
            else
            {
                switch (n)
                {
                    case 0:
                        {
                            Item1Object.SetActive(false);
                        }
                        break;
                    case 1:
                        {
                            Item2Object.SetActive(false);
                        }
                        break;
                    case 2:
                        {
                            Item3Object.SetActive(false);
                        }
                        break;
                }
            }
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

    bool ItemsetCheck(ItemName item1, ItemName item2 = ItemName.eITEM_INVALID, ItemName item3 = ItemName.eITEM_INVALID)
    {
        mIsDestory = true;
        mWaitDestroyFlame = 60;
        OrderItemList[(int)item1] -= 1;
        OrderItemList[(int)item2] -= 1;
        OrderItemList[(int)item3] -= 1;
        foreach (var item in OrderItemList)
        {
            if (item != 0)
            {
                //ここで×か〇かの画像を上に乗っける？
                return false;
            }
        }
        return true;
    }

}
