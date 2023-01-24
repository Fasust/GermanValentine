using System;
using TMPro;
using UnityEngine;

public class DayCounter : MonoBehaviour
{
    private TextMeshProUGUI text;
    void Start()
    {
        text = this.GetComponent<TextMeshProUGUI>();

        if (DateTime.Today == new DateTime(DateTime.Today.Year, 5, 1))
        {
            text.text = "It is the first of May! Let's get to Work";
            return;
        }

        text.text = daysTill().ToString() + " Days left until the first of may";
    }

    private int daysTill()
    {
        double daysTill;
        bool pastFirstMay = DateTime.Today > new DateTime(DateTime.Today.Year, 5, 1);

        if (pastFirstMay)
        {
            daysTill = (new DateTime(DateTime.Today.Year + 1, 5, 1) - DateTime.Today).TotalDays;
        }
        else
        {
            daysTill = (DateTime.Today - new DateTime(DateTime.Today.Year, 5, 1)).TotalDays;
        }

        return ((int)daysTill);
    }
}
