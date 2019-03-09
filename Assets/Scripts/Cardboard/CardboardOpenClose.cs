using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardboardOpenClose : MonoBehaviour
{
    private UnityEngine.SpriteRenderer SpriteRenderer = null;
    public UnityEngine.Sprite OpenSprite = null;

    public UnityEngine.Sprite SideClosedSprite = null;
    public UnityEngine.Sprite TopClosedSprite = null;

    public UnityEngine.Sprite SideTopClosedSprite = null;
    public UnityEngine.Sprite TopSideClosedSprite = null;

    private bool IsSideClosed = false;
    private bool IsTopClosed = false;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer = GetComponent<UnityEngine.SpriteRenderer>();
        if (SpriteRenderer == null) throw new System.Exception("No SpriteRenderer component.");

        SpriteRenderer.sprite = OpenSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseClickBySide()
    {
        if (IsSideClosed) return;

        if(IsTopClosed)
            SpriteRenderer.sprite = TopSideClosedSprite;
        else
            SpriteRenderer.sprite = SideClosedSprite;
        IsSideClosed = true;
    }

    public void OnMouseClickByTop()
    {
        if (IsTopClosed) return;

        if(IsSideClosed)
            SpriteRenderer.sprite = SideTopClosedSprite;
        else
            SpriteRenderer.sprite = TopClosedSprite;
        IsTopClosed = true;
    }
}
