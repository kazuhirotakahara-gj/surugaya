using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
	public Transform PauseText;

	private float Timer = 0;

	private void Update()
	{
		Timer += Time.deltaTime * 12;
		PauseText.Rotate(Vector3.forward, Mathf.Sin(Timer)/7);
	}

	public void OnResume()
	{
		gameObject.SetActive(false);
		CurrentLevel.GamePaused = false;
	}

	public void OnQuit()
	{
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Title");
	}

	public void OnRestart()
	{
		CurrentLevel.GameStarted = false;
		CurrentLevel.GamePaused = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Game");
	}

	public void StartPause()
	{
		if (CurrentLevel.GameStarted)
		{
			gameObject.SetActive(true);
			CurrentLevel.GamePaused = true;
		}
	}
}
