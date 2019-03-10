using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleFade : MonoBehaviour
{
    public UnityEngine.UI.Graphic TargetGraphic;
    public float FadeSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (TargetGraphic == null)
        {
            var graphics = gameObject.GetComponent<UnityEngine.UI.Graphic>();
            if(graphics != null)
            {
                TargetGraphic = graphics;
            }
        }

        Color color = TargetGraphic.color;
        color.a = 1.0f;
        TargetGraphic.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        Color color = TargetGraphic.color;
        color.a -= Time.deltaTime * FadeSpeed;
        color.a = Mathf.Max(color.a, 0.0f);
        TargetGraphic.color = color;

        if(color.a <= 0.0f)
        {
            gameObject.SetActive(false);
        }
    }
}
