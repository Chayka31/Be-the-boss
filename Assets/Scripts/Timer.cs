using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public int _time;
    public int _hours;
    public int _days;
    public int _season;
    public int _years;

    public float DuplSec;

    public bool is_lunch;
    public bool is_work_time;

    public TextMeshProUGUI timer_text;
    public string _time_s;

    public static Timer timer;

    public bool is_paused;
    public int TimeMode;

    public Image[] img;



    void Start()
    {
        _time = 30;
        _hours = 7;
        _time_s = _hours.ToString() + ":00";
        timer_text.text = _time_s;
        TimeMode = 1;
        ChangeTimeMode(TimeMode);
    }

    public void Awake()
    {
        timer = this;
    }

    public string CountTime(float h) 
    {
        float a = h / 10f;
        int b = (int)a;
        return _hours.ToString() + ":" + b + "0";
    }

    public IEnumerator StartTime() 
    {
        while(true) 
        {
            _time++;
            _time_s = CountTime(_time);
            timer_text.text = _time_s;
            if (_time == 60)
            {
                _hours++;
                _time = 0;
                timer_text.text = CountTime(_time);

                EventBus.OneHour?.Invoke();
                switch (_hours) 
                {
                    case 13: is_lunch = true; break;
                    case 14: is_lunch = false; break;
                    case 8: is_work_time = true; EventBus.StartDay?.Invoke(); break;
                    case 22: is_work_time = false; EventBus.EndDay?.Invoke(); break;
                }
                
            }
            if (_hours == 24)
            {
                _hours = 0;
                _days++;
            }
            if (_days == 7)
            {
                _season++;
                _days = 0;
            }
            if (_season == 4)
            {
                _season = 0;
                _years++;
            }
            yield return new WaitForSeconds(DuplSec);
        }
    }
    public void ChangeTimeMode(int TimeMode)
    {
       switch (TimeMode) 
       {
            case 0: StopAllCoroutines(); is_paused = true; break;
            case 1: DuplSec = 1; break;
            case 2: DuplSec = 0.5f ; break;
            case 3: DuplSec = 0.2f; break;
       }
        foreach (Image im in img)
        {
            im.color = Color.white;
        }
        img[TimeMode].color = Color.gray;
        img[TimeMode].transform.DOShakePosition(0.10f,5,50,90,false,true);

        if(TimeMode != 0 && is_paused) 
        {
            StartCoroutine(StartTime());
            is_paused = false;

        }
    }
}
