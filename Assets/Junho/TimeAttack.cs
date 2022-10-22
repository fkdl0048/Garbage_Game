using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TimeAttack : Singleton<TimeAttack>
{
    [SerializeField]
    private Slider timeSlider;
    [SerializeField]
    private GameObject handle;

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
                SceneManager.LoadScene("DeathEnding");
            }

            handle.transform.Rotate(0, 0, timeValue);
            timeSlider.value = timeValue / maxTime;
        }
    }

    private void FixedUpdate()
    {
        if (Spawner.Instance.isSpawn)
        TimeValue += Time.deltaTime;
    }
}
