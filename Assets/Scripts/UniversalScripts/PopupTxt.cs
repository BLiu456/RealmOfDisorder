using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupTxt : MonoBehaviour
{
    public TMP_Text msgTxt;

    void Awake()
    {
        msgTxt = this.GetComponent<TMP_Text>();
    }

    public void displayPopup(string msg)
    {
        StartCoroutine(display(msg));
    }

    private IEnumerator display(string msg)
    {
        msgTxt = this.GetComponent<TMP_Text>();
        msgTxt.text = msg;
        msgTxt.enabled = true;
        yield return new WaitForSeconds(1);
        msgTxt.enabled = false;
        Destroy(gameObject);
    }
}
