using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenu : MonoBehaviour
{
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Splash0");
    }

    public void OnClickExtraContents()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Splash1");
    }

    public void OnClickOptions()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Splash2");
    }
}
