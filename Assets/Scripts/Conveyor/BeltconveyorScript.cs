using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltconveyorScript : MonoBehaviour
{
    Conveyor _Conveyor;
    // Start is called before the first frame update
    void Start()
    {
        var parent = gameObject.transform.parent;
        _Conveyor = parent?.GetComponent<Conveyor>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_Conveyor == null)
            return;

        Vector3 nowposition = this.transform.position;

        nowposition.x += _Conveyor.speed * Time.deltaTime;
        this.transform.position = nowposition;

        if (nowposition.x < _Conveyor.endposition)
        {
            nowposition.x = _Conveyor.startposition +_Conveyor.speed * Time.deltaTime;
            this.transform.position = nowposition;
        }
    }
}
