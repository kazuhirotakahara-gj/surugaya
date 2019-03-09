using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float Speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var deletable = true;
        var image = TryGetItemImage();
        if(image != null)
        {
            if (image.IsMousePicking)
                deletable = false;
        }

        if (deletable == false)
            return;
    }

    ItemImage TryGetItemImage()
    {
        var itemTransform = transform.Find("ItemImage");
        if (itemTransform == null)
            return null;

        var itemImageObject = itemTransform.gameObject;
        var itemImageComponent = itemImageObject.GetComponent<ItemImage>();

        return itemImageComponent;
    }
}
