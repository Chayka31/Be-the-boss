using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sotrudniki : MonoBehaviour
{
    public List<GameObject> CellsSotrudniks = new List<GameObject>();
    public Transform content;

    public static Sotrudniki instance;
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnable()
    {
        SetGraphic();
        EventBus.OneHour += OneHourEnd;
    }
    public void OnDisable()
    {
        DestroyGraphic();
        EventBus.OneHour -= OneHourEnd;
    }


    public void DestroyGraphic()
    {   
        for(int i = 0; i < content.childCount; i++) 
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }

    public void OneHourEnd() 
    {
        foreach (CardCrafter item in GetComponentsInChildren<CardCrafter>())
        {
            item.ZapolnDescr();
        }
    }

    public void RefreshDannie() 
    {
        DestroyGraphic();
        SetGraphic();
    }
    public void SetGraphic() 
    {
        for(int i = 0; i < OfficeManager.instance.max_rabotniks; i++) 
        {
            if (i < OfficeManager.instance.rabotniks.Count) 
            {
                if (OfficeManager.instance.rabotniks[i] is Rabotnik)
                {
                    GameObject cl =  Instantiate(CellsSotrudniks[1], content);
                    cl.GetComponent<CardCrafter>().rb = OfficeManager.instance.rabotniks[i] as Rabotnik;
                }
            }
            else 
            {
                Instantiate(CellsSotrudniks[0], content);
            }
        }
    }
}
