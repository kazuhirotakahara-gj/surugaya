using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class GlobalScore
{
    static public int Score = 0;
}


public class ScoreScript : MonoBehaviour
{
    public int DispScore = 0;
    public int MoveSize = 0;

	public float FontAnimSpeed = 0.1f;

    public Text Label;

	public Animator GoodIcon;

    public ScoreData score = new ScoreData();

	private enum AnimState { None, Big, Stay, Small };
	private AnimState _AnimState = AnimState.None;
	private float _AnimTimer = 0;
	private int _InitFontSize = 0;
	private float _CurrentFontSize = 0;
    
    // Start is called before the first frame update
    void Start()
    {
		GoodIcon.GetComponent<SpriteRenderer>().enabled = false;
		GoodIcon.enabled = false;

		_InitFontSize = Label.fontSize;
		_CurrentFontSize = (float)_InitFontSize;
    }
    
    // Update is called once per frame
    void Update()
    {
        Label.text = DispScore.ToString();

		if (_AnimState != AnimState.None)
		{
			switch (_AnimState)
			{
				case AnimState.Big:
					{
						_AnimTimer += Time.deltaTime;
						if (_AnimTimer >= FontAnimSpeed)
						{
							Label.fontSize = _InitFontSize * 2;
							_AnimTimer = 0;
							_AnimState = AnimState.Stay;
						}
						else
						{
							Label.fontSize = (int)(_InitFontSize + _InitFontSize * (_AnimTimer / FontAnimSpeed));
						}
					}
					break;

				case AnimState.Stay:
					{
						_AnimTimer += Time.deltaTime;
						if (_AnimTimer >= FontAnimSpeed)
						{
							_AnimTimer = 0;
							_AnimState = AnimState.Small;
						}
					}
					break;

				case AnimState.Small:
					{
						_AnimTimer += Time.deltaTime;
						if (_AnimTimer >= FontAnimSpeed)
						{
							Label.fontSize = _InitFontSize;
							_AnimTimer = 0;
							_AnimState = AnimState.None;
						}
						else
						{
							Label.fontSize = (int)(_InitFontSize + _InitFontSize * (1-_AnimTimer / FontAnimSpeed));
						}
					}
					break;
			}
		}
    }

    void OnCardboardDispatched(Cardboard cardboard)
    {
        if (cardboard == null)
            return;

        if (cardboard.IsEmpty)
            ++score.Empty;

		if (cardboard.IsGoodjob)
		{
			++score.GoodJob;
			GoodIcon.GetComponent<SpriteRenderer>().enabled = true;
			GoodIcon.enabled = true;
			GoodIcon.Play("UI_Good", 0, 0);
			_AnimState = AnimState.Big;
		}

        DispScore = score.GoodJob;

        GlobalScore.Score = score.GoodJob;
    }
}
