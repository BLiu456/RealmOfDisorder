using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverUI;

    [SerializeField]
    private AudioSource bg;

    [SerializeField]
    private AudioSource goJingle;

    public void gameOver()
    {
        this.GetComponent<Timer>().pauseSwitch();
        this.GetComponent<GameMaster>().spawnOff();

        GameObject[] gameObj = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject x in gameObj)
        {
            x.SetActive(false);
        }

        GameObject[] pObj = GameObject.FindGameObjectsWithTag("Enemy_Atk");
        foreach (GameObject x in pObj)
        {
            Destroy(x);
        }

        gameOverUI.SetActive(true);
        bg.mute = !bg.mute;
        goJingle.Play();
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void quit()
    {
        Application.Quit();
    }
}
