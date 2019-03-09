using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardboardOpenClose : MonoBehaviour
{
    private UnityEngine.SpriteRenderer SpriteRenderer = null;
    public UnityEngine.Sprite OpenSprite = null;
    public UnityEngine.Sprite Close0Sprite = null;
    public UnityEngine.Sprite Close1Sprite = null;

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
        SpriteRenderer.sprite = Close0Sprite;
    }

    public void OnMouseClickByTop()
    {
        SpriteRenderer.sprite = Close1Sprite;
    }
}
