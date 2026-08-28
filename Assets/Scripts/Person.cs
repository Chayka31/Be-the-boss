using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class Person 
{
    public string Name { get; set; }
    public int Age { get; set; }

    public string Sex { get; set; }
    public int Staj {  get; set; }


    public Person() 
    {
        Age = Random.Range(18, 61);
        Staj = 0;
        Sex = Random.Range(0, 2) == 0 ? "Женщина" : "Мужчина";
        if (Sex == "Мужчина") 
        {
            Name = GetRandomLineFromFile("maleFname")+ " "+ GetRandomLineFromFile("maleLname");
        }
        else
        {
            Name = GetRandomLineFromFile("femaleFname") + " " + GetRandomLineFromFile("femaleLname");
        }
    }

    public abstract void OneHourRabotnik();


    public void ChangeName(string new_name) 
    {
        Name = new_name;
    }

    private string GetRandomLineFromFile(string fileName)
    {
        List<string> lines = new List<string>();

        // Чтение файла
        TextAsset textAsset = Resources.Load<TextAsset>(fileName);
        if (textAsset != null)
        {
            using (StringReader reader = new StringReader(textAsset.text))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            // Получение случайной строки
            if (lines.Count > 0)
            {
                int randomIndex = Random.Range(0, lines.Count);
                return lines[randomIndex];
            }
        }
        else
        {
            Debug.LogError("Файл не найден: " + fileName);
        }

        return null;
    }


}
