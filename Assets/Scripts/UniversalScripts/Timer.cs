using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float currTime;

    public TextMeshProUGUI timerText;

    void Start()
    {
        currTime = 0f;
    }

    void Update()
    {
        currTime += Time.deltaTime; 
        updateTimerText();
    }

    void updateTimerText()
    {
        int min = Mathf.FloorToInt(currTime / 60f);
        int sec = Mathf.FloorToInt(currTime - min * 60);

        string timeFormat = string.Format("{0:00}:{1:00}", min, sec);

        timerText.text = timeFormat;
    }

    public float getTime()
    {
        return currTime;
    }

    public int getMin()
    {
        int min = Mathf.FloorToInt(currTime / 60f);
        return min;
    }

    public int getSec()
    {
        int min = getMin();
        int sec = Mathf.FloorToInt(currTime - min * 60);
        return sec;
    }
}
