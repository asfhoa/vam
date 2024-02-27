using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopUI : Singleton<TopUI>
{
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text killCountText;
    [SerializeField] Image expFill;

    public void UpdateExp(float current,float max)
    {
        expFill.fillAmount = current / max;
    }

    public void UpdateLevel(int amount)
    {
        levelText.text = $"Lv.{amount}";
    }

    public void UpdateKillCount(int amount)
    {
        killCountText.text = amount.ToString("N0");
    }
}
