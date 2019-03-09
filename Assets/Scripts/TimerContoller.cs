using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerContoller : MonoBehaviour
{
    public float time = 0f;
    private float rorate_per_time = 0f;
    private float elapsed_time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rorate_per_time = 360 / time;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion nowrotation = this.transform.rotation;
        nowrotation.x = Time.deltaTime * rorate_per_time;
        this.transform.rotation = nowrotation;

    }
}
