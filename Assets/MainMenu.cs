using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject startUI;

    [SerializeField]
    private GameObject controlUI;

    // Start is called before the first frame update
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void HowToPlay()
    {
        startUI.SetActive(!startUI.activeSelf);
        controlUI.SetActive(!controlUI.activeSelf);
    }
}
