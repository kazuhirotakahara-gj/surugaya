using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private float _BaseMoveSpeed = 2.0f;

    public bool AutoMove = false;

    public Vector3 MoveDir = Vector3.left;

    private Vector3 EndPosition = Vector3.left;

    private bool FirstMove = true;

	[HideInInspector]
	public LevelController _LevelController;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
		if (CurrentLevel.GamePaused)
		{
			return;
		}

        var deletable = false;
        if (AutoMove)
        {
            if(FirstMove)
            {
                EndPosition = transform.localPosition;
                EndPosition.x = -transform.localPosition.x;
                FirstMove = false;
            }

            transform.localPosition += MoveDir * _LevelController.ConveyorMoveSpeed * _BaseMoveSpeed * Time.deltaTime;

            if(transform.localPosition.x < EndPosition.x)
                deletable = true;
        }

        var image = TryGetItemImage();
        if(image != null)
        {
            if (image.IsMousePicking)
                deletable = false;
        }

        if (deletable == false)
            return;

        GameObject.Destroy(gameObject);
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
