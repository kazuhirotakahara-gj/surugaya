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

    public enum Status { NoClose, SideClosed, TopClosed, SideTopClosed, TopSideClosed };

    private bool IsTopClosed = false;
    private bool IsSideClosed = false;
    private bool IsTopSideClosed = false;
    private bool IsSideTopClosed = false;

    private bool IsFullClosed
    {
        get { return IsTopClosed && IsSideClosed; }
    }

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
    }

    void Update()
    {
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
}
