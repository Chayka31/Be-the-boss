using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class OfficeManager : MonoBehaviour
{
    public int max_rabotniks;
    [SerializeField] public List<object> rabotniks; 

    public float _money;

    public static OfficeManager instance;

    public float Charizma;

    public void OnEnable()
    {
        EventBus.OneHour += OneHourOffice;
        EventBus.StartDay += StartDayOffice;
        EventBus.EndDay += EndDayOffice;
    }

    public void OnDisable()
    {
        EventBus.OneHour -= OneHourOffice;
        EventBus.StartDay -= StartDayOffice;
        EventBus.EndDay -= EndDayOffice;
    }

    public void StartDayOffice() 
    {
        foreach (var item in rabotniks)
        {
            if (item is Rabotnik rb)
            {
                rb.StartDay();
            }
        }
    }

    public void OneHourOffice()
    {
        foreach (var item in rabotniks) 
        {
            if (item is Rabotnik rb) 
            {
                rb.OneHourRabotnik();
            }
        }
    }

    public void EndDayOffice()
    {
        foreach (var item in rabotniks)
        {
            if (item is Rabotnik rb)
            {
                rb.EndDay();
            }
        }
    }


    void Awake()
    {
        rabotniks = new List<object>();
        instance = this;
    }
    public void Update()
    {

    }

}
