using System.Collections;
using System.Collections.Generic;
using UnityEngine;


static public class CurrentLevel
{
    static public GameObject[] CondidateItems = null;
}

public class SelectLevel : MonoBehaviour
{
    public GameObject[] EasyCondidateItems = new GameObject[] { };
    public GameObject[] NormalCondidateItems = new GameObject[] { };
    public GameObject[] HardCondidateItems = new GameObject[] { };

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
        CurrentLevel.CondidateItems = EasyCondidateItems;
    }

    public void OnClickNormal()
    {
        DecisionSound.Play();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Game");
        CurrentLevel.CondidateItems = NormalCondidateItems;
    }

    public void OnClickHard()
    {
        DecisionSound.Play();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Game");
        CurrentLevel.CondidateItems = HardCondidateItems;
    }
}
