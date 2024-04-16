using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupGenerator : MonoBehaviour
{
    public GameObject popPrefab;
  
    public void generatePopup(string msg)
    {
        GameObject popup = Instantiate(popPrefab, transform.position, Quaternion.identity);
        popup.GetComponent<PopupTxt>().displayPopup(msg);
        popup.transform.parent = transform; //Make this popup a child of the generator
    }
}
