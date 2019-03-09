using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemImage : MonoBehaviour
{
    public enum ItemImageState
    {
        Invalid,

        PickMouse,

        ReleaseMouse,

        AtBox,

        AtBelt,
    }

    public bool IsMousePicking
    {
        get
        {
            return State == ItemImageState.PickMouse;
        }
    }

    public PurchaseOrderScript.ItemName Name = PurchaseOrderScript.ItemName.eITEM_INVALID;

    public ItemImageState State;

    // Start is called before the first frame update
    void Start()
    {
        State = ItemImageState.AtBelt;
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// MouseでPickしたい
    /// </summary>
    public bool TryMousePick()
    {
        if (State != ItemImageState.AtBelt)
            return false;

        State = ItemImageState.PickMouse;
        return true;
    }

    /// <summary>
    /// MouseのPickをキャンセル
    /// </summary>
    public bool TryMouseRelease()
    {
        if (State != ItemImageState.PickMouse)
            return false;

        State = ItemImageState.AtBelt;

        // 補完いる？
        transform.localPosition = Vector3.zero;
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    public void TryOutBox()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public bool TryInBox(ItemJunction junc)
    {
        var trans = junc?.ItemImages?.transform;
        if (trans == null)
            return false;

        gameObject.transform.parent = trans;
        State = ItemImageState.AtBox;
        var sprite = gameObject.GetComponent<SpriteRenderer>();
        if(sprite != null)
            sprite.sortingOrder = 41;

        return true;
    }
}
