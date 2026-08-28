using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RenamePersonal : MonoBehaviour
{
    public TextMeshProUGUI StartText;
    public TMP_InputField inputField;
    public object _sotrudnik;

    public void SetGraphic(Person p) 
    {

        StartText.text = p.Name;
        _sotrudnik = p;
    }
    public void ChangeNameSotr() 
    {
        switch (_sotrudnik.GetType().ToString()) 
        {
            case "Rabotnik": Rabotnik r = _sotrudnik as Rabotnik; r.ChangeName(inputField.text); break;
        }
        
        Sotrudniki.instance.RefreshDannie();
        Exit();
        
    }
    public void Exit() 
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
