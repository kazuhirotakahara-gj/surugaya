using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreScript : MonoBehaviour
{
    public uint DispScore = 0;
    public uint DestScore = 0;
    public uint MoveSize = 0;

    public string PreviosScoreText = "";
    public string RearScoreText = "";
    public Text Label;
    
    // Start is called before the first frame update
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        if(DispScore < DestScore)
        {
            DispScore += MoveSize;
            if(DispScore > DestScore)
            {
                DispScore = DestScore;
            }
        }

        Label.text = PreviosScoreText + DispScore.ToString() + RearScoreText;
    }

    public void AddScore(uint AddScore)
    {
        DestScore += AddScore;
    }
}
