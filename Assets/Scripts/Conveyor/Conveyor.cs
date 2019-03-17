using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
	public SpriteRenderer BeltboardSpriteRenderer;

	public uint _TiledBeltMaxNum = 40;
	private float _BeltWidth = 0;

	public float _MoveSpeed = 1;
	private float _BaseMoveSpeed = 4;
	private float _MovedDistance = 0;

	private LevelController _LevelController;

	private void Awake()
	{
		if (BeltboardSpriteRenderer == null)
		{
			throw new System.Exception("Beltboard SpriteRender is null.");
		}

		SetupBeltboard();

        var gos = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (var go in gos)
        {
            if (go != null && go.transform.parent == null)
            {
                var lc = go.GetComponent<LevelController>();
                if (lc)
                {
                    _LevelController = lc;
                }
            }
        }
	}

	private void Update()
	{
		MoveBeltboard();
	}

	private void SetupBeltboard()
	{
		var size = BeltboardSpriteRenderer.size;
		_BeltWidth = size.x * 2;
		size.x *= _TiledBeltMaxNum;
		BeltboardSpriteRenderer.size = size;

		BeltboardSpriteRenderer.enabled = true;
	}

	private void MoveBeltboard()
	{
		if (CurrentLevel.GamePaused)
		{
			return;
		}

		var moveDistance = Time.deltaTime * _BaseMoveSpeed * _LevelController.ConveyorMoveSpeed;

		var size = BeltboardSpriteRenderer.size;
		size.x += moveDistance;

		_MovedDistance += moveDistance;
		if (_MovedDistance >= _BeltWidth)
		{
			size.x -= _BeltWidth;
			_MovedDistance -= _BeltWidth;
		}

		BeltboardSpriteRenderer.size = size;
	}
}
