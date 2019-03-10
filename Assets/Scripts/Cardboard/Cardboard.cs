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

    public GameObject PurchaseHolder;

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

    public PurchaseOrderScript PurchaseOrder
    {
        get
        {
            if (!IsScreenOver)
                return null;

            if (PurchaseHolder == null)
                return null;
            
            var order = PurchaseHolder.GetComponentsInChildren<PurchaseOrderScript>();
            if (order.Length == 0)
                return null;

            return order[0];
        }
    }

    public bool IsEmpty
    {
        get
        {
            if (Items == null || Items.Count == 0)
                return true;

            return false;
        }
    }

    public bool IsGoodjob
    {
        get
        {
            if (PurchaseOrder == null)
                return false;

            var retValue = PurchaseOrder.CheckOrder(Items);
            return retValue;
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

            InvisibleImages();

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

    void InvisibleImages()
    {
        noCloseObj.SetActive(false);
        sideClosedObj.SetActive(false);
        topClosedObj.SetActive(false);
        sideTopClosedObj.SetActive(false);
        topSideClosedObj.SetActive(false);
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
            float buffer = 200;
            if((0-buffer < screenPos.x) || (screenPos.x <Screen.width+buffer) || (0-buffer < screenPos.y) || (screenPos.y < Screen.height+buffer))
            {
                IsScreenOver = true;

                var gos = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));
                foreach(var go in gos)
                {
                    if(go != null && go.transform.parent == null)
                    {
                        go.BroadcastMessage("OnCardboardDispatched", this, SendMessageOptions.DontRequireReceiver);
                    }
                }
                Flinging = false;
                InvisibleImages();

                DisablePurchaseOrder();
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
            AudioManager.Instance?.CallSE(AudioManager.SE_Type.MakeBox2);
            OutBoxCollider.SetActive(true);
            DisableItems();
        }
        else
        {
            CurrentStatus = Status.SideClosed;
            AudioManager.Instance?.CallSE(AudioManager.SE_Type.MakeBox1);
        }
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
            AudioManager.Instance?.CallSE(AudioManager.SE_Type.MakeBox2);
            OutBoxCollider.SetActive(true);
            DisableItems();
        }
        else
        {
            CurrentStatus = Status.TopClosed;
            AudioManager.Instance?.CallSE(AudioManager.SE_Type.MakeBox1);
        }
    }

    void DisableItems()
    {
        var itemImages = gameObject.GetComponentsInChildren<ItemImage>();
        foreach(var itemImage in itemImages)
        {
            itemImage.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            itemImage.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void DisablePurchaseOrder()
    {
        var purchaseOrders = gameObject.GetComponentsInChildren<PurchaseOrderScript>();
        foreach(var purchaseOrder in purchaseOrders)
        {
            var renders = purchaseOrder.gameObject.GetComponentsInChildren<SpriteRenderer>();
            foreach(var render in renders)
            {
                render.enabled = false;
            }
        }
    }

    private Vector3 CalcDragPos()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = 1;
        pos = Camera.main.ScreenToWorldPoint(pos);
        pos.z = initPosition.z;
        return pos;
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
        AudioManager.Instance?.CallSE(AudioManager.SE_Type.SendBox);
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
