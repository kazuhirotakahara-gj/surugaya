using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseOrderTimer : MonoBehaviour
{
	public PurchaseOrderSpawner Spawner;
	private UnityEngine.UI.Text _Text;
	private UnityEngine.UI.Outline _Outline;
	private Color _InitOutlineColor;

	private float WarningTimer = 0;

    void Start()
    {
		_Text = GetComponent<UnityEngine.UI.Text>();
		_Outline = GetComponent<UnityEngine.UI.Outline>();
		_InitOutlineColor = _Outline.effectColor;
    }

    void Update()
    {
		var lastPO = Spawner.lastPurchaseOrder;
		if (lastPO != null && !lastPO.IsOutBoxed && lastPO.DisplayLimitTimer > 0.0f)
		{
			_Text.enabled = true;
			var timer = lastPO.DisplayLimitTimer;
			_Text.text = string.Format("{0:0.00}", timer);
		}
		else
		{
			_Text.enabled = false;
		}

		var ratio = lastPO.DisplayLimitTimer / lastPO.DisplayLimitInitialTimer;
		if (ratio <= 0.5f)
		{
			ratio = 0.5f - ratio;
			WarningTimer += Time.deltaTime * 30;
			var s = Mathf.Abs(Mathf.Sin(WarningTimer)) * ratio;
			var scale = new Vector3(1 + s, 1 + s, transform.localScale.z);
			transform.localScale = scale;
			_Text.color = new Color(1, 1-ratio * 2, 1-ratio * 2);
		}
		else
		{
			var scale = new Vector3(1, 1, transform.localScale.z);
			transform.localScale = scale;
			_Text.color = Color.white;
			_Outline.effectColor = _InitOutlineColor;
		}
    }
}
