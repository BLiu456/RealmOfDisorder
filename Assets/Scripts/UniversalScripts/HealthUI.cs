using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthUI : MonoBehaviour
{
    [SerializeField]
    private Health playerHlth;

    [SerializeField]
    private TextMeshProUGUI healthText;

    [SerializeField]
    private Image healthBar;

    public void changeBar()
    {
        float currHp = (float) playerHlth.getCurrentHp();
        float maxHp = (float) playerHlth.getMaxHp();
     
        healthBar.fillAmount = currHp / maxHp;

        string txt = string.Format("{0}/{1}", currHp, maxHp);
        healthText.text = txt;  
    }
}
