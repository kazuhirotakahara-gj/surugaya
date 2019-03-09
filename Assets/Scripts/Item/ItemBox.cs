using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public float BoxSize = 1.0f;

    public Vector3 BoxOffset = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(var i = 0; i < transform.childCount; ++i)
        {
            switch(transform.childCount)
            {
                case 1:
                    {
                        var child = transform.GetChild(i);
                        child.transform.localPosition = BoxOffset;
                    }
                    break;
                case 2:
                    {
                        var child = transform.GetChild(i);
                        switch(i)
                        {
                            case 0:
                                child.transform.localPosition = BoxOffset + Vector3.left * BoxSize;
                                break;
                            case 1:
                                child.transform.localPosition = BoxOffset + Vector3.right * BoxSize;
                                break;
                        }
                    }
                    break;
                case 3:
                    {
                        var child = transform.GetChild(i);
                        switch (i)
                        {
                            case 0:
                                child.transform.localPosition = BoxOffset + Vector3.left * BoxSize;
                                break;
                            case 1:
                                child.transform.localPosition = BoxOffset + Vector3.right * BoxSize;
                                break;
                            case 2:
                                child.transform.localPosition = BoxOffset;
                                break;
                        }
                    }
                    break;
            }
        }
    }
}
