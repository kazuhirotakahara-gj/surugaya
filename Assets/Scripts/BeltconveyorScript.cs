using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltconveyorScript : MonoBehaviour
{
    public float startposition = 0;
    public float endposition = 0;
    public float speed = 0;

    Conveyor _Conveyor;
    // Start is called before the first frame update
    void Start()
    {
        var parent = gameObject.transform.parent;
        _Conveyor = parent?.GetComponent<Conveyor>();
        //startposition = 10;//Conveyor1.transform.position.x + Conveyor1.GetComponent<RectTransform>().sizeDelta.x;
        //endposition = -10;
    }

    // Update is called once per frame
    void Update()
    {
        if (_Conveyor == null)
            return;

        Vector3 nowposition = this.transform.position;

        nowposition.x += speed;
        this.transform.position = nowposition;

        if (nowposition.x < endposition)
        {
            nowposition.x = startposition;
            this.transform.position = nowposition;
        }
    }
}
