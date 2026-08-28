using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI txt_money;
    public TextMeshProUGUI txt_countS;
    public TextMeshProUGUI txt_charizma;
    public List<GameObject> screens = new List<GameObject>();
    public int CurrentOpenScreen;
    public Image FadeImage;
    public GameObject DayResults;
    public List<Sprite> sprites;

    public Image TimerImage;
    public Sprite DayImage;
    public Sprite NightImage;

    public void OnEnable()
    {
        EventBus.StartDay += ChangeDayNight;
        EventBus.EndDay += ChangeDayNight;
    }

    public void OnDisable()
    {

    }

    void Start()
    {
        foreach (GameObject go in screens)
        {
            go.transform.localScale = new Vector3(0, 1, 1);
            go.SetActive(true);
        }
        CurrentOpenScreen = -1;

        FadeImage.gameObject.SetActive(true);
        FadeImage.color = new Color(0f, 0f, 0f, 1f);
        Invoke("FadeExit", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        txt_money.text = OfficeManager.instance._money.ToString("F1");
        txt_countS.text = OfficeManager.instance.rabotniks.Count.ToString() + "/" + OfficeManager.instance.max_rabotniks.ToString();
        txt_charizma.text = (OfficeManager.instance.Charizma* 100f).ToString() + "%";
    }

    public async void ChangeDayNight() 
    {
        FadeEnter();
        await Task.Delay(1125);
        if (Timer.timer.is_work_time) //  ночь -> день 
        {
            TimerImage.sprite = DayImage;
            Sotrudniki.instance.RefreshDannie();
        }
        else //  день -> ночь
        {
            TimerImage.sprite = NightImage;
            Timer.timer.ChangeTimeMode(0);
            await CheckAllSotrudniks();
            Sotrudniki.instance.RefreshDannie();
        }
        FadeExit();
    }


    public async Task CheckAllSotrudniks()
    {
        foreach (object r in OfficeManager.instance.rabotniks)
        {
            await DoDayEnd(r);
        }
        await Task.Delay(1000);
    }

    public async Task DoDayEnd(object sotrudnik)
    {
        if (sotrudnik is Rabotnik rb)
        { 

            Image Stats = DayResults.transform.GetChild(0).GetComponent<Image>();
            TextMeshProUGUI Name = Stats.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            Image Img = Stats.transform.GetChild(1).GetComponent<Image>();
            TextMeshProUGUI main = Stats.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            Img.sprite = rb.Sex == "Мужчина" ? sprites[0] : sprites[1];

            Stats.DOFade(0, 0f);
            Name.DOFade(0, 0f);
            Img.DOFade(0, 0f);
            main.DOFade(0, 0f);

            Name.text = rb.Name;
            main.text = $"Работник заработал за этот день {(int)rb._zarabotannie} В. зарплата составляет {(int)rb._zpDay} В. ";
            await Task.Delay(1000);
            DayResults.SetActive(true);
            Stats.DOFade(1, 0.3f).OnComplete(() => { Name.DOFade(1, 0.3f).OnComplete(() => { Img.DOFade(1, 0.3f).OnComplete(() => { main.DOFade(1, 0.3f); }); }); });

            await Task.Delay(3000);
            Image Chanse = DayResults.transform.GetChild(1).GetComponent<Image>();
            Image GreenFon = Chanse.transform.GetChild(0).GetComponent<Image>();
            Image RedFon = GreenFon.transform.GetChild(0).GetComponent<Image>();
            TextMeshProUGUI TextChance = Chanse.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            GameObject strelka = Chanse.transform.GetChild(2).transform.GetChild(0).gameObject;

            Chanse.DOFade(0, 0f);
            GreenFon.DOFade(0, 0f);
            RedFon.DOFade(0, 0f);
            TextChance.DOFade(0, 0f);
            strelka.GetComponent<Image>().DOFade(0, 0f);
            RedFon.fillAmount = 0f; 

            TextChance.text = "Шанс провала: " + rb._len/2 + "%";
            await Task.Delay(1000);

            Chanse.gameObject.SetActive(true);
            Chanse.DOFade(1, 0.5f).OnComplete(() => { GreenFon.DOFade(1, 0.5f).OnComplete(() => { TextChance.DOFade(1, 0.3f).OnComplete(() => { RedFon.DOFade(1, 0.3f).OnComplete(() => { RedFon.DOFillAmount((rb._len/2f)/100f,1f); }); }); }); });

            await Task.Delay(3000);
            strelka.gameObject.SetActive(true);
            strelka.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, 20f, 0f);

            await RandomizerChance(rb._len, rb, strelka.GetComponent<RectTransform>());

            Chanse.gameObject.SetActive(false);
            DayResults.SetActive(false);

        }
    }

    public async Task RandomizerChance(int len, Rabotnik rb, RectTransform tr)
    {
        int a = Random.Range(0, 101);
        tr.gameObject.SetActive(true);
        tr.gameObject.GetComponent<Image>().DOFade(1, 0.3f);
        tr.DOAnchorPosX(300, 1f);
        await Task.Delay(1100);
        tr.DOAnchorPosX((a/100f) * 300, 0.5f).OnComplete(() => { tr.DOAnchorPosY(-20f, 0.5f); });
        bool UD = a > len / 2;
        switch (UD) 
        {
            case true: rb.LuckyWork(); break;
            case false: rb.UnLuckyWork(); break;
        }
        await Task.Delay(2000);
    }





    public void OpenButton(int i)
    {
        if (screens[i].transform.localScale.x == 1 || screens[i].transform.localScale.x == 0) 
        {
            if (i == CurrentOpenScreen)
            {
                GoLeftScreen(screens[i]);
                CurrentOpenScreen = -1;
                return;
            }
            else
            {
                for (int j = 0; j < screens.Count; j++)
                {
                    if (screens[i] != screens[j]) 
                    {
                        GoLeftScreen(screens[j]);
                    }
                }
                GoRightScreen(screens[i]);
                CurrentOpenScreen = i;
            }
        }
    }

    public void FadeEnter() 
    {
        FadeImage.gameObject.SetActive(true);
        FadeImage.DOFade(1, 0.35f);
    }

    public void FadeExit()
    {
        FadeImage.DOFade(0, 0.35f).OnComplete(() => { FadeImage.gameObject.SetActive(false); });
    }


    public void GoLeftScreen(GameObject screen) 
    {
        screen.transform.DOScaleX(0, 0.25f).OnComplete(() => {screen.SetActive(false);});
    }
    public void GoRightScreen(GameObject screen)
    {
        screen.SetActive(true);
        screen.transform.DOScaleX(1, 0.25f);
    }
}
