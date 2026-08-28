using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TESTgenerator : MonoBehaviour
{
    
    [SerializeField]public Rabotnik rabotnik;
    public TextMeshProUGUI tmpro;

    public void Awake()
    {
        rabotnik = new Rabotnik();
        tmpro.text = rabotnik.ShowInformation();
        
    }

    public void Regenerate() 
    {
        tmpro.text = new Rabotnik().ShowInformation();
    }

}
