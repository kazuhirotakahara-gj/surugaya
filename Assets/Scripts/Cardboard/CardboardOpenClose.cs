using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardboardOpenClose : MonoBehaviour
{
    private UnityEngine.SpriteRenderer SpriteRenderer = null;
    public UnityEngine.Sprite OpenSprite = null;
    public UnityEngine.Sprite CloseSprite = null;

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

    public void OnMouseClick()
    {
        SpriteRenderer.sprite = CloseSprite;
    }
}
