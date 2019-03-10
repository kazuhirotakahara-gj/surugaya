using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevel : MonoBehaviour
{
    public AudioSource DecisionSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickEasy()
    {
        DecisionSound.Play();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Game");
    }

    public void OnClickNormal()
    {
        DecisionSound.Play();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Game");
    }

    public void OnClickHard()
    {
        DecisionSound.Play();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Game");
    }
}
