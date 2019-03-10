using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenu : MonoBehaviour
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

    public void OnClickCampaignMode()
    {
        DecisionSound.Play();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/SelectLevel");
    }

    public void OnClickExtraContents()
    {
        DecisionSound.Play();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/ExtraContents");
    }

    public void OnClickOptions()
    {
        DecisionSound.Play();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Options");
    }

    public void OnClickCredits()
    {
        DecisionSound.Play();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Credits");
    }
}
