using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreScript : MonoBehaviour
{
    public int DispScore = 0;
    public int MoveSize = 0;

    public string PreviosScoreText = "";
    public string RearScoreText = "";
    public Text Label;

    public ScoreData score = new ScoreData();
    
    // Start is called before the first frame update
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        Label.text = PreviosScoreText + DispScore.ToString() + RearScoreText;
    }

    void OnCardboardDispatched(Cardboard cardboard)
    {
        Debug.Log("dispatched.");

        if (cardboard.Items.Count == 0)
        {
            ++score.Empty;
            DispScore -= 100;
        }

        foreach (var item in cardboard.Items)
        {
            DispScore += 10;
        }
    }
}
