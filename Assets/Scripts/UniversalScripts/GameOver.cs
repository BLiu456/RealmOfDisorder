using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverUI;

    public void gameOver()
    {
        this.GetComponent<Timer>().pauseSwitch();
        this.GetComponent<GameMaster>().spawnOff();

        GameObject[] gameObj = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject x in gameObj)
        {
            x.SetActive(false);
        }
      
        gameOverUI.SetActive(true);
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
