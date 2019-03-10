using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    private float CountTime = 4f;
    float timecount = 0f;

    public GameObject Obj_3;
    public GameObject Obj_2;
    public GameObject Obj_1;
    public GameObject Obj_start;
    public GameObject Watch_hands;

    private bool Is3Displayed = false;
    private bool Is2Displayed = false;
    private bool Is1Displayed = false;
    private bool IsStart = false;

    TimerContoller _TimerContoller;

    // Start is called before the first frame update
    void Start()
    {
        timecount = CountTime;
        Obj_3.SetActive(true);
        _TimerContoller = Watch_hands.GetComponent<TimerContoller>();
    }

    // Update is called once per frame
    void Update()
    {
        if(0<= timecount)
        {
            if (1 <= timecount && Input.GetMouseButtonDown(0))
                timecount = (int)(timecount);
            else
                timecount -= Time.deltaTime;
        }


        if (timecount < 0)
        {
            if (IsStart == false)
            {
                _TimerContoller.TimerState = TimerContoller.State.Start;
                Obj_3.SetActive(false);
                Obj_2.SetActive(false);
                Obj_1.SetActive(false);
                Obj_start.SetActive(false);
            }

            IsStart = true;

        }
        else if (timecount < 1)
        {
            Obj_3.SetActive(false);
            Obj_2.SetActive(false);
            Obj_1.SetActive(false);
            Obj_start.SetActive(true);
        }
        else if (timecount < 2)
        {
            Obj_3.SetActive(false);
            Obj_2.SetActive(false);
            Obj_1.SetActive(true);
            Obj_start.SetActive(false);

        }
        else if(timecount < 3)
        {
            Obj_3.SetActive(false);
            Obj_2.SetActive(true);
            Obj_1.SetActive(false);
            Obj_start.SetActive(false);
        }
    }
}
