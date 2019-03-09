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
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Game");
    }

    public void OnClickExtraContents()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Test/DragAndDropTest");
    }

    public void OnClickOptions()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Options");
    }

    public void OnClickCredits()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Credits");
    }
}
