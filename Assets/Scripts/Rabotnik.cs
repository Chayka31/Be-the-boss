using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;



public class Rabotnik : Person
{
    [Header("Основные характеристики")]
    public int _bazovaya_stavka;
    public float _productivnost;
    public float _nastroy;
    public int _len;

    [Header("Доп характеристики")]
    public int _AddLenEveryHour;
    public int _streaknastroy;
    public bool is_oplachen;
    public float _zarabotannie;
    public float _zpDay;

    public void StartDay() 
    {
        if (!is_oplachen)
        {
            if (_len < 50) 
            {
                _nastroy = 0.5f;
                _len = 50;
                _streaknastroy = 0;
            }
            else 
            {
                _nastroy = 0.5f;
            }
        }
        else 
        {
            _zpDay = 0;
        }
        _zarabotannie = 0;
    }

    public void UnLuckyWork() 
    {
        _nastroy = 1;
        _streaknastroy = 0;
    }

    public void LuckyWork() 
    {
        _nastroy += 0.25f;
        _streaknastroy++;
    }

    public override void OneHourRabotnik()
    {
        if (!Timer.timer.is_work_time)
        {
            return;
        }

        _zarabotannie += _bazovaya_stavka * _productivnost;

        if (Timer.timer.is_lunch )
        {
            return;
        }
        OfficeManager.instance._money += _bazovaya_stavka * _productivnost;
        _len += _AddLenEveryHour;
        _productivnost = SearchProd();
    }

    public void EndDay()
    {
        is_oplachen = false;
        _zpDay += _zarabotannie * 0.25f + _bazovaya_stavka;

    }
    
    public float SearchProd() 
    {
        return (_nastroy * (1 - _len/100) * OfficeManager.instance.Charizma) - (_nastroy * (1 - _len / 100) * OfficeManager.instance.Charizma) / 100 * Age / 1.25f + (_nastroy * (1 - _len / 100) * OfficeManager.instance.Charizma) / 100 * Staj / 1.5f;
    }

    public Rabotnik() 
    {

        _bazovaya_stavka = Random.Range(10, 26);
        _nastroy = (float)System.Math.Round(Random.Range(1, 1.3f),2);
        _len = Random.Range(0,11);
        _productivnost = SearchProd();
        _AddLenEveryHour = 1;
        _streaknastroy = 0;
        _zpDay = 0;
        _zarabotannie = 0;
        is_oplachen = true;
    }
    public string ShowPerson() 
    {
        return "Имя: " + Name + "\n" +
               "Возраст: " + Age + "\n" +
               "Пол: " + Sex;
    }

    public string ShowBazovaya() 
    {
        return "Базовая ставка: " + _bazovaya_stavka;
    }

    public string ShowInformation() 
    {
        return "Имя: " + Name + "\n" +
            "Возраст: " + Age + "\n" +
            "Пол: " + Sex + "\n" +
            "Базовая ставка: " + _bazovaya_stavka + "\n" +
            "Настрой: " + _nastroy + "\n" +
            "Лень: " + _len + "%" + "\n" +
            "Продуктивность: " + (float)System.Math.Round(_productivnost, 2) + "\n";
            
    }
}
