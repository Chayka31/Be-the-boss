using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.UI;
using System;

public class ViborTypeSotrudnik : MonoBehaviour
{
    public GameObject prefab_buySotr;
    public Transform canvas;
    public bool is_selected;

    public Transform StartT;
    public Transform EndT;

    public Transform _types;
    public List<GameObject> ViborsPrefabs = new List<GameObject>();
    public List<GameObject> ViborsCreated = new List<GameObject>();

    public int currenttype;


    public void Start()
    {
        canvas = FindObjectOfType<Canvas>().gameObject.transform;
    } 

    public void ClickOnBack() 
    {
        if (!is_selected) 
        {
            transform.GetChild(0).DOMove(StartT.position, 0.5f);
            is_selected = true;
            StartCoroutine(StartVibor());

            
        }
        else 
        {
            transform.GetChild(0).DOMove(EndT.position, 0.5f);
            is_selected = false;
            
            StopAllCoroutines();
            foreach (var vib in ViborsCreated) 
            {
                Destroy(vib.gameObject);
            }
        }
    }

    public IEnumerator StartVibor() 
    {
        for(int i = 0; i < ViborsPrefabs.Count; i++)
        {
            yield return new WaitForSeconds(0.25f);
            GameObject vBtn = Instantiate(ViborsPrefabs[i], _types);
            vBtn.name = i.ToString();
            Button button = vBtn.GetComponent<Button>();
            int currentindex = i;
            button.onClick.AddListener(() => TypeVibran(currentindex));
            ViborsCreated.Add(vBtn);
        }
    }
    public void TypeVibran(int i) 
    {
        Debug.Log(i);
        switch (i) 
        {
            case 0:
                GameObject bs = Instantiate(prefab_buySotr, canvas);
                bs.transform.GetChild(0).GetComponent<BuySotrudnik>()._type = "Работник";
                break;
            case 1:
                GameObject ub = Instantiate(prefab_buySotr, canvas);
                ub.transform.GetChild(0).GetComponent<BuySotrudnik>()._type = "Уборщик";
                break;
        }
    }
}
