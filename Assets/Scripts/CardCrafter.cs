using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardCrafter : MonoBehaviour
{
    public GameObject Face;
    public GameObject Back;

    public GameObject Rename_prefab;
    public GameObject PrefabOplata;
    private GameObject oplataobj;

    public string _type;

    public TextMeshProUGUI NameCard;
    public Image ImageRabotnik;
    public TextMeshProUGUI Description;

    public Sprite[] sprites;
    public Rabotnik rb;

    public Image backimg;
    public Color opl_color;
    public Color neopl_color;

    [Header("Работник")]
    public TextMeshProUGUI DescriptionPerson;
    public TextMeshProUGUI DescriptionClass;
    public TextMeshProUGUI DescriptionLen;
    public Image imglen;
    public TextMeshProUGUI DescriptionNatroy;
    public TextMeshProUGUI DescriptionNatroyStreak;


    void Start()
    {
        ZapolnDescr();
    }

    public void ZapolnDescr()
    {
        if (_type == "Работник")
        {
            if (rb == null)
            {
                rb = new Rabotnik();
                Description.text = rb.ShowInformation();

                NameCard.text = _type;
                ImageRabotnik.sprite = rb.Sex == "Мужчина" ? sprites[0] : sprites[1];
                Description.text = rb.ShowInformation();
            }
            else
            {
                NameCard.text = _type;
                ImageRabotnik.sprite = rb.Sex == "Мужчина" ? sprites[0] : sprites[1];
                DescriptionPerson.text = rb.ShowPerson();
                DescriptionClass.text = rb.ShowBazovaya();
                DescriptionLen.text = rb._len + "%";
                imglen.fillAmount = rb._len/100f;
                DescriptionNatroy.text = rb._nastroy.ToString();
                DescriptionNatroyStreak.text = "Стрик: " + rb._streaknastroy.ToString();
                if (rb.is_oplachen) 
                {
                    backimg.color = opl_color;
                }
                else 
                {
                    backimg.color = neopl_color;
                }

                if (!rb.is_oplachen) 
                {
                    if (oplataobj == null) 
                    {
                        oplataobj = Instantiate(PrefabOplata, Back.transform);
                        oplataobj.GetComponent<Button>().onClick.AddListener(delegate { OplataSotrudnik(); });
                        oplataobj.GetComponentInChildren<TextMeshProUGUI>().text = $"Выдать зарплату: {(int)rb._zpDay}";
                    }
                }
                else 
                {
                    if (oplataobj != null)
                    {
                        Destroy(oplataobj);
                    }
                }

            }
        }
    }

    public void UvolitSotr()
    {
        for (int i = 0; i < OfficeManager.instance.rabotniks.Count; i++)
        {
            if (OfficeManager.instance.rabotniks[i] == rb)
            {
                OfficeManager.instance.rabotniks.RemoveAt(i);
            }
        }
        GameObject.FindObjectOfType<Sotrudniki>().RefreshDannie();
    }
    public void ReverseFace()
    {
        Face.transform.DOLocalRotate(new Vector3(0, 90, 0), 0.20F).OnComplete(() =>
        {
            Face.SetActive(false);
            Back.SetActive(true);
            Back.transform.DOLocalRotate(new Vector3(0, 90, 0), 0);
            Back.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.20f);
        });
    }
    public void ReverseBack()
    {
        Back.transform.DOLocalRotate(new Vector3(0, 90, 0), 0.20F).OnComplete(() =>
        {
            Back.SetActive(false);
            Face.SetActive(true);
            Face.transform.DOLocalRotate(new Vector3(0, 90, 0), 0);
            Face.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.20f);
        });
    }



    public void InstRenameSotr() 
    {
        RenamePersonal r = Instantiate(Rename_prefab, GameObject.Find("Canvas").transform).GetComponentInChildren<RenamePersonal>();
        r.SetGraphic(rb);
    }

    public void OplataSotrudnik() 
    {
        if (OfficeManager.instance._money >= rb._zpDay) 
        {
            OfficeManager.instance._money -= rb._zpDay;
            rb.is_oplachen = true;
            ZapolnDescr();
        }

    }

    public void NaimSotrudnik() 
    {
        OfficeManager.instance.rabotniks.Add(rb);
        Destroy(transform.parent.parent.gameObject);
        GameObject.FindObjectOfType<Sotrudniki>().RefreshDannie(); 
    }
}
