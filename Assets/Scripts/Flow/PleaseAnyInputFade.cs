using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PleaseAnyInputFade : MonoBehaviour
{
    CanvasGroup canvasGroup;
    public float MinAlpha = 0.5f;
    public float FadeSpeed = 1.0f;
    private float FadeTimer = 0.0f;

    private enum TitleStatus { PressAny, SelectGame };
    private TitleStatus Status = TitleStatus.PressAny;

    public GameObject NextActiveMenu;
    public AudioSource SelectSound;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void UpdatePressAny()
    {
        FadeTimer += Time.deltaTime * FadeSpeed;
        canvasGroup.alpha = (Mathf.Sin(FadeTimer) * 0.5f + 0.5f)* (1.0f - MinAlpha) + MinAlpha;

        if(Input.anyKey)
        {
            SelectSound.Play();
            gameObject.SetActive(false);
            NextActiveMenu.SetActive(true);
        }
    }

    void UpdateSelectGame()
    {
    }

    // Update is called once per frame
    void Update()
    {
        switch(Status)
        {
            case TitleStatus.PressAny:
                UpdatePressAny();
                break;

            case TitleStatus.SelectGame:
                UpdateSelectGame();
                break;
        }
    }
}
