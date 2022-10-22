using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeAttack : Singleton<TimeAttack>
{
    [SerializeField]
    private Slider timeSlider;

    [SerializeField] private float maxTime;

    private float timeValue;
    public float TimeValue
    {
        get { return timeValue; }
        set 
        {
            timeValue = value;
            if(timeValue < 0)
            {
                timeValue = 0;
            }
            if (timeValue > maxTime)
            {
                //event
            }

            timeSlider.value = timeValue / maxTime;
        }
    }

    private void FixedUpdate()
    {
        TimeValue += Time.deltaTime;
    }
}
