using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private TimeSpan timer = new TimeSpan();
    public Text seconds;

    private void Update()
    {
        timer = timer.Add(TimeSpan.FromSeconds(Time.deltaTime));
        seconds.text = timer.Seconds.ToString("00");
        if (seconds.text == "120")
        {
            SceneManager.LoadScene("End");
        }
    }
}
