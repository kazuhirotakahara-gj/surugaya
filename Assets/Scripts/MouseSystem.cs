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

    Vector3 _MouseDownPosition = Vector3.zero;

    Vector3 _LastPosition = Vector3.zero;

    public MouseState State = MouseState.Invalid;

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

        if (Trace)
            Debug.Log("MouseDrag:" + pos.ToString());

        return;
    }

    void OnMyMouseUp(Vector3 pos)
    {
        _MouseDownPosition = Vector3.zero;
        State = MouseState.MouseMove;

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

    }
}
