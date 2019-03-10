using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayGood : MonoBehaviour
{
    public float CountTime = 0f;
    float timecount = 0f;
    public GameObject GJ;
    bool HideGJ = false;

    // Start is called before the first frame update
    void Start()
    {
        timecount = CountTime;
        GJ.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        timecount -= Time.deltaTime;
        if (timecount < 0 || HideGJ)
        {
            GJ.SetActive(false);
        }
    }
}
