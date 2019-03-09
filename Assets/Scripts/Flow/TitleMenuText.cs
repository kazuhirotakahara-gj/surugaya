using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenuText : MonoBehaviour
{
    UnityEngine.UI.Text text;
    public SelectMenu selectMenu;
    private Color initColor;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<UnityEngine.UI.Text>();
        initColor = text.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseEnter()
    {
        text.color = selectMenu.SelectingColor;
    }

    public void OnMouseExit()
    {
        text.color = initColor;
    }
}
