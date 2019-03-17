using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSystem : MonoBehaviour
{
    public enum MouseState
    {
        Invalid,

        MouseMove,

        MouseDown,

        MouseUp,

        MouseDrag
    }

    public bool Trace = true;

    //Rayの長さ
    public float _MaxDistance = 500;

    Vector3 _MouseDownScreenPosition = Vector3.zero;

    /// <summary>
    /// 1f前のマウス座標
    /// </summary>
    Vector3 _LastMousePoint = Vector3.zero;

    public GameObject PickObject = null;

    public MouseState State = MouseState.Invalid;

    public LayerMask _ItemMask = new LayerMask() { value = 1 };

    public LayerMask _InBoxMask = new LayerMask() { value = 9 };

    public LayerMask _OutBoxMask = new LayerMask();

    public LayerMask _PurchaseMask = new LayerMask();

    public ItemImage PickItemImageComponent;
    public PurchaseOrderScript PickPurchaseOrderComponent;

    public bool IsItemPicked
    {
        get
        {
            return PickItemImageComponent != null;
        }
    }

    public bool IsPurchaseOrderPicked
    {
        get
        {
            return PickPurchaseOrderComponent != null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        State = MouseState.MouseMove;
        _LastMousePoint = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
		if (!CurrentLevel.GameStarted || CurrentLevel.GamePaused)
		{
			return;
		}

        var newPos = Input.mousePosition;
        switch (State)
        {
            case MouseState.MouseMove:
                {
                    OnMyMouseMove(newPos);
                }
                break;
            case MouseState.MouseDown:
                {
                    OnMyMouseDown(newPos);
                }
                break;
            case MouseState.MouseUp:
                {
                    OnMyMouseUp(newPos);
                }
                break;
            case MouseState.MouseDrag:
                {
                    OnMyMouseDrag(newPos);
                }
                break;
        }

        _LastMousePoint = newPos;
    }


    void OnMyMouseDown(Vector3 pos)
    {
        if (Input.GetMouseButton(0))
            State = MouseState.MouseDrag;
        else
            State = MouseState.MouseUp;

        var go = RayCastItem(pos, _PurchaseMask);
        if (go == null)
        {
            go = RayCastItem(pos, _ItemMask);
            if(go != null)
                AudioManager.Instance?.CallSE(AudioManager.SE_Type.ItemPick);
        }
        else
        {
            AudioManager.Instance?.CallSE(AudioManager.SE_Type.OrderPick);
        }

        SetPickObject(go);
        if (Trace)
            Debug.Log("MouseDown:" + pos.ToString());
    }

    void OnMyMouseDrag(Vector3 pos)
    {
        if (Input.GetMouseButton(0) == false)
            State = MouseState.MouseUp;

        // ドラッグ中は必ず上書きしたい
        //if (_LastMousePoint == pos)
        //    return;
        {
            var synced = SyncPickObjectPos(pos);
            if (Trace && !synced)
                Debug.Log("MouseDrag:" + pos.ToString());
        }

        return;
    }

    void OnMyMouseUp(Vector3 pos)
    {
        _MouseDownScreenPosition = Vector3.zero;

        // とりあえず放す
        if (PickObject != null)
        {
            if (IsItemPicked)
            {
                var inbox = RayCastItem(pos, _InBoxMask);
                if (inbox != null)
                {
                    var junc = inbox.GetComponent<ItemJunction>();
                    PickItemImageComponent.TryInBox(junc);
                    AudioManager.Instance?.CallSE(AudioManager.SE_Type.ItemPut);
                }
                else
                {
                    AudioManager.Instance?.CallSE(AudioManager.SE_Type.ItemPut);
                }
            }

            if (IsPurchaseOrderPicked)
            {
                var outbox = RayCastItem(pos, _OutBoxMask);
                if (outbox != null)
                {
                    var junc = outbox.GetComponent<ItemJunction>();
                    PickPurchaseOrderComponent.TryOutBox(junc);
                    AudioManager.Instance?.CallSE(AudioManager.SE_Type.OrderPut);
                }
                else
                {
                    AudioManager.Instance?.CallSE(AudioManager.SE_Type.OrderPut);
                }
            }
        }


        SetPickObject(null);

        if (Trace)
            Debug.Log("MouseUp:" + pos.ToString());

        State = MouseState.MouseMove;
        return;
    }

    void OnMyMouseMove(Vector3 pos)
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            _MouseDownScreenPosition = Camera.main.ScreenToViewportPoint(pos);
            State = MouseState.MouseDown;
        }

        if (_LastMousePoint == pos)
            return;

        if (Trace)
            Debug.Log("MouseMove:" + pos.ToString());

        return;
    }

    GameObject RayCastItem(Vector3 pos, LayerMask mask)
    {
        //メインカメラ上のマウスカーソルのある位置からRayを飛ばす
        var ray = Camera.main.ScreenPointToRay(pos);
        var hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, _MaxDistance, mask);

        //なにかと衝突した
        if (hit.collider)
        {
            var go = hit.collider.gameObject;
            return go;
        }

        return null;
    }

    void SetPickObject(GameObject obj)
    {
        if (IsItemPicked)
            PickItemImageComponent.TryMouseRelease();

        if (IsPurchaseOrderPicked)
            PickPurchaseOrderComponent.TryMouseRelease();

        PickItemImageComponent = null;
        PickPurchaseOrderComponent = null;
        PickObject = null;

        if (obj == null)
            return;


        var order = obj?.GetComponent<PurchaseOrderScript>();
        if (order != null)
        {
            if (order.TryMousePick())
            {
                PickPurchaseOrderComponent = order;
                PickObject = obj;
                return;
            }
        }

        var img = obj?.GetComponent<ItemImage>();
        if (img != null)
        {
            if (img.TryMousePick())
            {
                PickObject = obj;
                PickItemImageComponent = img;
                return;
            }
        }

        return;
    }

    bool SyncPickObjectPos(Vector3 pos)
    {
        if (PickObject == null)
            return false;

        if (PickObject != null)
        {
            var s_pos = Camera.main.ScreenToWorldPoint(pos + Vector3.forward);
            PickObject.transform.position = s_pos;

            if (Trace)
                Debug.Log("Pick WorldPos:" + pos.ToString());
        }
        return true;
    }
}
