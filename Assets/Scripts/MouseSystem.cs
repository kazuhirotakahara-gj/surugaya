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

    Vector3 _MouseDownPosition = Vector3.zero;

    Vector3 _LastPosition = Vector3.zero;

    /// <summary>
    /// マウスダウンした直後において
    /// pickしたアイテムと、マウスの位置の差
    /// </summary>
    Vector3 _PickItemMouseDiff = Vector3.zero;

    public GameObject PickItemImageObject = null;

    public MouseState State = MouseState.Invalid;

    public LayerMask _Mask;

    public ItemImage PickItemImageComponent;

    // Start is called before the first frame update
    void Start()
    {
        State = MouseState.MouseMove;
        _LastPosition = Input.mousePosition;
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

        _LastPosition = newPos;
    }


    void OnMyMouseDown(Vector3 pos)
    {
        if (Input.GetMouseButton(0))
            State = MouseState.MouseDrag;
        else
            State = MouseState.MouseUp;

        RayCastItem(pos);
        if(Trace)
            Debug.Log("MouseDown:" + pos.ToString());

        return;
    }

    void OnMyMouseDrag(Vector3 pos)
    {
        if(Input.GetMouseButton(0) == false)
            State = MouseState.MouseUp;

        if (_LastPosition == pos)
            return;

        SyncPickItemImageObjectPos(pos);
        if (Trace)
            Debug.Log("MouseDrag:" + pos.ToString());

        return;
    }

    void OnMyMouseUp(Vector3 pos)
    {
        _MouseDownPosition = Vector3.zero;
        State = MouseState.MouseMove;

        // とりあえず放す
        SetPickItemImageObject(null);

        if (Trace)
            Debug.Log("MouseUp:" + pos.ToString());
        return;
    }

    void OnMyMouseMove(Vector3 pos)
    {
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            _MouseDownPosition = pos;
            State = MouseState.MouseDown;
        }

        if (_LastPosition == pos)
            return;

        if (Trace)
            Debug.Log("MouseMove:" + pos.ToString());

        return;
    }

    void RayCastItem(Vector3 pos)
    {
        //メインカメラ上のマウスカーソルのある位置からRayを飛ばす
        var ray = Camera.main.ScreenPointToRay(pos);
        var hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, _MaxDistance, _Mask);

        //なにかと衝突した時だけそのオブジェクトの名前をログに出す
        if (hit.collider)
        {
            var go  = hit.collider.gameObject;
            SetPickItemImageObject(go);
        }
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

            _PickItemMouseDiff      = obj.transform.position - _MouseDownPosition;
        }
        else
        {
            PickItemImageObject     = null;
            PickItemImageComponent  = null;

            _PickItemMouseDiff      = Vector3.zero;
        }

    }

    void SyncPickItemImageObjectPos(Vector3 pos)
    {
        if (PickItemImageObject == null)
            return;

        PickItemImageObject.transform.position = pos;
        //PickItemImageObject.transform.position = pos + _PickItemMouseDiff;
    }
}
