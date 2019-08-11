using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class NoteTimer : MonoBehaviour
{

    private int seconds;
    private string timeString;
    private int counter;
    private bool countdown;
    private bool done;
    private bool started;
    private bool flashOn;

    AudioSource audioData;

    // Start is called before the first frame update
    void Start()
    {
        seconds = 0;
        countdown = false;
        timeString = "";
        done = false;
        flashOn = true;
        audioData = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (counter < 50)
        {
            counter++;
        }
        else
        {
            if (countdown)
            {
                counter = 0;
                if (seconds > 0)
                {
                    seconds -= 1;
                }
                if (seconds <= 0 && started)
                {
                    done = true;
                    audioData.Play(0);
                }
                if (done)
                {
                    if (flashOn)
                    {
                        transform.parent.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
                        flashOn = false;
                    }
                    else
                    {
                        transform.parent.GetComponent<Image>().color = new Color32(220, 150, 10, 255);
                        flashOn = true;
                    }

                }
            }
            
        }

        transform.GetChild(4).GetComponent<Text>().text = GetStringTime(seconds);
        //print(seconds);
    }

    public void AddTime(int seconds)
    {
        this.seconds += seconds;
    }

    public void StartTimer()
    {
        countdown = true;
        started = true;
        transform.GetChild(3).GetComponent<Image>().color = new Color32(220, 150, 10, 255);

    }

    private string GetStringTime(int seconds)
    {
        int minutes = seconds / 60;
        int hours = minutes / 60;

        string time = hours.ToString("D2") + ":" + minutes.ToString("D2") + ":" + seconds.ToString("D2");
        return time;
    }

}
