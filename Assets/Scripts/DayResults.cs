using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DayResults : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CheckAllSotrudniks();
    }

    public async void CheckAllSotrudniks() 
    {
        foreach(object r in OfficeManager.instance.rabotniks) 
        {
            await DoDayEnd(r);
        }
    }

    public async Task DoDayEnd(object sotrudnik) 
    {
        if (sotrudnik is Rabotnik rb) 
        {
            await Task.Delay(1000);
            Debug.Log($"Сотрудник {rb.Name} заработал за этот день {rb._zarabotannie} и просит зарплату {rb._zpDay} ");
            await Task.Delay(3000);
            Debug.Log($"Удачно ли поработал сотрудник {rb.Name} c ленью {rb._len} : {RandomizerChance(rb._len)}");
            await Task.Delay(3000);
        }
    }

    public bool RandomizerChance(int len) 
    {
        int a = Random.Range(0, 101);
        Debug.Log($"a = {a} < {len / 2}");
        return (a < len / 2);
    }
}
