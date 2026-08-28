using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;
using YG;
using UnityEditor.SearchService;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private int currentlist;
    public List<GameObject> lists = new List<GameObject>();

    public string boss_name;
    public string boss_sex;

    public TMP_InputField inp;

    [SerializeField] private int _age;
    [SerializeField] private int _charizma;
    [SerializeField] private int _maxpod4;

    public TextMeshProUGUI _h1;
    public TextMeshProUGUI _h2;
    public TextMeshProUGUI _h3;

    public TextMeshProUGUI _s;

    public Sprite[] sprites;
    public Image imagefon;

    public GameObject txtname;

    public void ChangeName() 
    {
        boss_name = inp.text;
    }
    public void ToSelectList(int i) 
    {
        if (currentlist != i) 
        {
            foreach (GameObject list in lists)
            {
                list.transform.localScale = Vector3.zero;
                list.SetActive(false);
            } 

            currentlist = i;
            lists[i].SetActive(true);
            lists[i].transform.DOScale(1, 0.5f);
        }
    }
    void Start()
    {
        ToSelectList(1);
        ToSelectList(0);
        boss_sex = "Мужчина";
        RandomizeStartHaracteristic();
    }


    public void ClickToReklama() 
    {
        YandexGame.RewVideoShow(0);
    }
    public void RandomizeStartHaracteristic() 
    {
        _age = Random.Range(18, 60);
        _charizma = Random.Range(100, 201);
        _maxpod4 = Random.Range(1, 4);

        _h1.text = "Возраст: " + _age;
        _h2.text = "Харизма: " + _charizma + "%";
        _h3.text = "Макс подчиненных: " + _maxpod4;
    }
    public void ChangeSexBoss() 
    {
        switch (boss_sex) 
        {
            case "Мужчина": boss_sex = "Женщина"; imagefon.sprite = sprites[1]; break;
            case "Женщина": boss_sex = "Мужчина"; imagefon.sprite = sprites[0]; break;
        }
        imagefon.gameObject.transform.localScale = Vector3.zero;
        imagefon.transform.DOScale(Vector3.one, 0.3f);
        _s.text = boss_sex;
    }

    public void OpedTelegram() 
    {
        Application.OpenURL("https://t.me/chae4ka31");
    }
    public void OpedWk()
    {
        Application.OpenURL("https://vk.com/id402981015");
    }
    public void OpedDs()
    {
        Application.OpenURL("https://vk.com/id402981015");
    }

    public void StartGame() 
    {
        if (boss_name != "") 
        {
            SceneManager.LoadScene(1);
        }
        else 
        {
            txtname.transform.DOShakePosition(0.2f, 5, 15);
        }
    }

}
