using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cardboard : MonoBehaviour
{
    public GameObject noCloseObj;
    public GameObject sideClosedObj;
    public GameObject topClosedObj;
    public GameObject sideTopClosedObj;
    public GameObject topSideClosedObj;

    public GameObject TopCollider;
    public GameObject SideCollider;
    public GameObject InBoxCollider;
    public GameObject OutBoxCollider;

    public GameObject ItemsHolder;

    public enum Status { NoClose, SideClosed, TopClosed, SideTopClosed, TopSideClosed };

    public bool IsScreenOver = false;

    private bool IsTopClosed = false;
    private bool IsSideClosed = false;
    private bool IsTopSideClosed = false;
    private bool IsSideTopClosed = false;

    private bool IsFullClosed
    {
        get { return IsTopClosed && IsSideClosed; }
    }

    public List<ItemImage> Items
    {
        get
        {
            if (!IsScreenOver) return null;

            var items = new List<ItemImage>();
            ItemsHolder.GetComponentsInChildren<ItemImage>(items);
            return items;
        }
    }

    private Queue<float> dragSpeedQueue = new Queue<float>();
    private Vector3 lastDragPosition = Vector3.zero;

    private Vector3 initPosition = Vector3.zero;

    public float FlingSpeedRatio = 0.5f;
    private bool Flinging = false;
    private float FlingSpeed = 0.0f;
    private Vector3 LastFlingVector;
    public float FlingSpeedAccel = 100.0f;

    private Status status = Status.NoClose;
    public Status CurrentStatus
    {
        get
        {
            return status;
        }
        set
        {
            status = value;

            noCloseObj.SetActive(false);
            sideClosedObj.SetActive(false);
            topClosedObj.SetActive(false);
            sideTopClosedObj.SetActive(false);
            topSideClosedObj.SetActive(false);

            switch(status)
            {
                case Status.NoClose: noCloseObj.SetActive(true); break;
                case Status.SideClosed: sideClosedObj.SetActive(true); break;
                case Status.TopClosed: topClosedObj.SetActive(true); break;
                case Status.SideTopClosed: sideTopClosedObj.SetActive(true); break;
                case Status.TopSideClosed: topSideClosedObj.SetActive(true); break;
            }
        }
    }

    void Start()
    {
        CurrentStatus = Status.NoClose;
        initPosition = transform.position;
    }

    void Update()
    {
        if(Flinging)
        {
            var additionalPos = LastFlingVector * Time.deltaTime;
            additionalPos.z = 0;
            transform.position += additionalPos * FlingSpeed;

            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            float buffer = 100;
            if((0-buffer < screenPos.x) || (screenPos.x <Screen.width+buffer) || (0-buffer < screenPos.y) || (screenPos.y < Screen.height+buffer))
            {
                IsScreenOver = true;
                BroadcastMessage("OnCardboardDispatched", this, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    public void OnMouseClickBySide()
    {
        if (IsFullClosed && !IsSideClosed) return;
        IsSideClosed = true;
        SideCollider.SetActive(false);
        InBoxCollider.SetActive(false);

        if (IsTopClosed)
        {
            CurrentStatus = Status.TopSideClosed;
            OutBoxCollider.SetActive(true);
        }
        else
            CurrentStatus = Status.SideClosed;
    }

    public void OnMouseClickByTop()
    {
        if (IsFullClosed && !IsTopClosed) return;
        IsTopClosed = true;
        TopCollider.SetActive(false);
        InBoxCollider.SetActive(false);

        if (IsSideClosed)
        {
            CurrentStatus = Status.SideTopClosed;
            OutBoxCollider.SetActive(true);
        }
        else
            CurrentStatus = Status.TopClosed;
    }

    private Vector3 CalcDragPos()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = 1;
        return Camera.main.ScreenToWorldPoint(pos);
    }

    public void OnDrag()
    {
        var oldPos = transform.position;
        var currentPos = CalcDragPos();
        var speed = Vector3.Distance(currentPos, oldPos);
        dragSpeedQueue.Enqueue(speed);
        if(dragSpeedQueue.Count >= 10)
        {
            dragSpeedQueue.Dequeue();
        }

        transform.position = currentPos;
        LastFlingVector = currentPos - oldPos;
    }

    public void OnDragBegin()
    {
        lastDragPosition = CalcDragPos();
    }

    public void OnEndDrag()
    {
        if (dragSpeedQueue.Count == 0) return;

        float totalSpeed = 0;
        foreach(float speed in dragSpeedQueue)
        {
            totalSpeed += speed;
        }
        float avgSpeed = totalSpeed / dragSpeedQueue.Count;

        if(avgSpeed < FlingSpeedRatio)
        {
            transform.position = initPosition;
        }
        else
        {
            Flinging = true;
            FlingSpeed = avgSpeed * FlingSpeedAccel;
            OutBoxCollider.SetActive(false);
        }
    }
}
