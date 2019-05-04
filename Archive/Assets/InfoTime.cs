using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoTime : MonoBehaviour
{

    TextMeshProUGUI TimeText;
    int timeStarted;
    public int seconds;
    public int minutes;

    bool run;

    // Start is called before the first frame update
    void Start()
    {
        TimeText = GetComponent<TextMeshProUGUI>();
        run = false;
        minutes = 0;
        seconds = 0;
    }

    public void ToggleTime()
    {
        if (run)
            run = false;
        else
            run = true;

        timeStarted = (int)Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (run == true)
        {
            minutes = (int)(Time.time - timeStarted) / 60;

            if ((int)(Time.time - timeStarted) >= 60)
            {
                seconds = ((int)(Time.time - timeStarted) - (minutes * 60));
            }
            else
            {
                seconds = (int)(Time.time - timeStarted);
            }

            TimeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }
}
