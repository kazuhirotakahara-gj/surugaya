using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
	public UnityEngine.UI.Text GoodNumLabel;

    // Start is called before the first frame update
    void Start()
    {
		GoodNumLabel.text = GlobalScore.Score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.anyKeyDown)
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Title");
		}
    }
}
