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
        transform.position = Vector3.zero;
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
    public void TryInBox()
    {

    }
}
