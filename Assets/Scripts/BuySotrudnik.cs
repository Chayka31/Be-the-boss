using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class BuySotrudnik : MonoBehaviour
{
    public GameObject PrefabCard;
    public string _type;
    public int countVariants;
    public List<CardCrafter> cards = new List<CardCrafter>();

    public BuySotrudnik(string type) 
    {
        _type = type;
    }
    void Start()
    {
        for (int i = 0; i < countVariants; i++) 
        {
            cards.Add(Instantiate(PrefabCard, this.gameObject.transform).GetComponent<CardCrafter>());
            cards[i]._type = _type;
        }
    }

    public void DestroyPanel() 
    {
        int i = Random.Range(0, countVariants+1);
        cards[i].NaimSotrudnik();
        Destroy(transform.parent.gameObject);
    }



}
