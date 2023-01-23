using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Timer : MonoBehaviour
{
    public float timeLeft = 10.0f;
    public float commandTime = 2.0f;
    public GameObject loseTimer;
    public TextMeshProUGUI count;
    public TextMeshProUGUI Intro;

    void Start()
    {
    loseTimer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        count.text = (timeLeft).ToString("0");
        if (timeLeft < 0)
        {
          loseTimer.SetActive(true);
        }
         commandTime -= Time.deltaTime;
        Intro.text = (commandTime).ToString("0");
        if (commandTime > 0)
        {
          Intro.text = " ";
        }
        if (commandTime < 0)
        {
          Intro.text = "";
          
        }
        if (timeLeft>0)
        {
          timeLeft -= Time.deltaTime; 
        }
    }
}