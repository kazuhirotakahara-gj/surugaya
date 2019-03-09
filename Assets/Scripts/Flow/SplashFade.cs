using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashFade : MonoBehaviour
{
    public UnityEngine.UI.Graphic TargetGraphics;

    private enum FadeStatus { Wait, FadeIn, Keep, FadeOut, End };
    private FadeStatus Status = FadeStatus.Wait;

    private Color CurrentColor;

    public float FadeSpeed = 1.0f;

    public float WaitTime = 1.0f;
    private float WaitTimer = 0.0f;

    public float KeepTime = 1.0f;
    private float KeepTimer = 0.0f;

    public string NextScene = "";

    public float TargetAlpha
    {
        get
        {
            return TargetGraphics.color.a;
        }

        set
        {
            Color color = TargetGraphics.color;
            color.a = Mathf.Clamp01(value);
            TargetGraphics.color = color;

            if (Status == FadeStatus.FadeIn && color.a >= 1.0f) Status = FadeStatus.Keep;
            else if (Status == FadeStatus.FadeOut && color.a <= 0.0f) Status = FadeStatus.End;
        }
    }

    void Start()
    {
        TargetAlpha = 0;
        WaitTimer = WaitTime;
        KeepTimer = KeepTime;
    }

    void Update()
    {
        switch(Status)
        {
            case FadeStatus.Wait:
                WaitTimer -= Time.deltaTime;
                if (WaitTimer <= 0.0f) Status = FadeStatus.FadeIn;
                if (Input.anyKeyDown)
                {
                    TargetAlpha = 1.0f;
                    Status = FadeStatus.Keep;
                }
                break;

            case FadeStatus.FadeIn:
                TargetAlpha += Time.deltaTime * FadeSpeed;
                KeepTimer = KeepTime;
                if (Input.anyKeyDown)
                {
                    TargetAlpha = 1.0f;
                    Status = FadeStatus.Keep;
                }
                break;

            case FadeStatus.Keep:
                KeepTimer -= Time.deltaTime;
                if (KeepTimer <= 0.0f) Status = FadeStatus.FadeOut;
                if (Input.anyKeyDown) Status = FadeStatus.End;
                break;
               
            case FadeStatus.FadeOut:
                TargetAlpha -= Time.deltaTime * FadeSpeed;
                if (Input.anyKeyDown) Status = FadeStatus.End;
                break;

            case FadeStatus.End:
                if (!string.IsNullOrEmpty(NextScene))
                {
                    SceneManager.LoadScene(NextScene);
                }
                break;
        }
    }
}
