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

    public GameObject PickItemImageObject = null;

    public MouseState State = MouseState.Invalid;

    public LayerMask _ItemMask = new LayerMask() { value = 1 };

    public LayerMask _BoxMask = new LayerMask() { value = 9 };

    public ItemImage PickItemImageComponent;

    // Start is called before the first frame update
    void Start()
    {
        State = MouseState.MouseMove;
        _LastMousePoint = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
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

        var go = RayCastItem(pos, _ItemMask);
        if(Trace)
            Debug.Log("MouseDown:" + pos.ToString());

        SetPickItemImageObject(go);
    }

    void OnMyMouseDrag(Vector3 pos)
    {
        if(Input.GetMouseButton(0) == false)
            State = MouseState.MouseUp;

        // ドラッグ中は必ず上書きしたい
        //if (_LastMousePoint == pos)
        //    return;

        var synced = SyncPickItemImageObjectPos(pos);
        if (Trace && !synced)
            Debug.Log("MouseDrag:" + pos.ToString());

        return;
    }

    void OnMyMouseUp(Vector3 pos)
    {
        _MouseDownScreenPosition = Vector3.zero;

        // とりあえず放す
        if (PickItemImageObject != null)
        {
            var box = RayCastItem(pos, _BoxMask);
            if (box != null)
            {
                var junc = box.GetComponent<ItemJunction>();
                PickItemImageComponent.TryInBox(junc);
            }
        }


        SetPickItemImageObject(null);

        if (Trace)
            Debug.Log("MouseUp:" + pos.ToString());

        State = MouseState.MouseMove;
        return;
    }

    void OnMyMouseMove(Vector3 pos)
    {
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
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

        //なにかと衝突した時だけそのオブジェクトの名前をログに出す
        if (hit.collider)
        {
            var go  = hit.collider.gameObject;
            return go;
        }

        return null;
    }

    void SetPickItemImageObject(GameObject obj)
    {
        if (PickItemImageObject == null && obj == null)
            return;

        if(PickItemImageComponent != null)
            PickItemImageComponent.TryMouseRelease();

        var img = obj?.GetComponent<ItemImage>();
        var result = false;

        if (img != null)
            result = img.TryMousePick();

        if(result)
        {
            PickItemImageObject     = obj;
            PickItemImageComponent  = img;
        }
        else
        {
            PickItemImageObject     = null;
            PickItemImageComponent  = null;
        }

    }

    bool SyncPickItemImageObjectPos(Vector3 pos)
    {
        if (PickItemImageObject == null)
            return false;

        var s_pos = Camera.main.ScreenToWorldPoint(pos + Vector3.forward);
        PickItemImageObject.transform.position = s_pos;

        if(Trace)
            Debug.Log("Pick WorldPos:" + pos.ToString());

        return true;
    }
}
