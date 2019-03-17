using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerContoller : MonoBehaviour
{
    public float time = 0f;
    private float rorate_per_time = 0f;

    private float degree = 0;
    private bool CalledTimerEnd = false;

    public State TimerState = State.Invalid;

    public enum State
    {
        Invalid = 0,

        Start = 1,

        TimeCount = 2,

        End = 3,

        Wait = 4
    }

    // Start is called before the first frame update
    void Start()
    {
        rorate_per_time = 360 / time;
        degree = 0;

        TimerState = State.Wait;
    }

    // Update is called once per frame
    void Update()
    {
		if (CurrentLevel.GamePaused)
		{
			return;
		}

        if (TimerState == State.Start || TimerState == State.Wait)
        {
            degree = 0;
            CalledTimerEnd = false;

            if (TimerState == State.Start)
                TimerState = State.TimeCount;
        }
        else if (TimerState == State.TimeCount)
        {
            CalledTimerEnd = false;
            degree += Time.deltaTime * rorate_per_time * -1;
            if (-360 > degree)
            {
                degree = 360;
                TimerState = State.End;
            }

            this.transform.eulerAngles = new Vector3(0, 0, degree);
        }
        else if (TimerState == State.End)
        {
            if (CalledTimerEnd == false)
            {
                OnTimerEnd();
            }
        }
    }

    public void SetWait()
    {
        TimerState = State.Wait;
    }

    public void SetStart()
    {
        TimerState = State.Start;
    }

    void OnTimerEnd()
    {
		CalledTimerEnd = true;
    }

	private uint forceFinishReqNum = 0;
	public void OnForceFinish()
	{
		uint limit = 5;
		if (forceFinishReqNum < limit)
		{
			++forceFinishReqNum;
			if (forceFinishReqNum >= limit)
			{
				degree = -359.9f;
			}
		}
	}
}
