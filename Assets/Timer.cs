using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class Timer : MonoBehaviour
{

private TMP_Text textClock;
private string hour, minute, second;
private DateTime time;
private PausedMenu Menu;
    void Awake(){
        textClock= GetComponent<TMP_Text>();
        time=DateTime.Now;
        hour =LeadingZero(time.Hour);
        minute = LeadingZero(time.Minute);
        second= LeadingZero(time.Second);
        textClock.text= hour+":"+minute;
        Menu = FindObjectOfType<PausedMenu>();
    }
   

    // Update is called once per frame
    void Update()
    {   
        if(Menu.isPaused!=true)
        {
        time=DateTime.Now;
        if (minute=="59" && second != LeadingZero(time.Second))
        {
            var change=int.Parse(hour)+1;
            hour=change.ToString();
            if (hour=="24")
            {
                hour="00";
            }
        }
        if (LeadingZero(time.Second)!=second)
        {   
            var newMinute=int.Parse(minute)+1;
            if (newMinute==60)
            {
                newMinute=0;
            }
            minute = LeadingZero(newMinute);
            second= LeadingZero(time.Second);
        }
        textClock.text= hour+":"+minute;
        }
    }
    string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2,'0');
    }
}
