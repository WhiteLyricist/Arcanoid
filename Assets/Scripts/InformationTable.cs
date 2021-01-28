using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InformationTable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string Scene = SceneManager.GetActiveScene().name; ;
        GetComponent<Text>().text = Scene + " , Esc - выход, Space - пауза, для быстрого перехода между уровнями нажмите 1 для перехода на 1 уровень, 2 на второй, 3 на третий.";
        if (Scene == "Level 3") 
        {
            GetComponent<Text>().text = Scene + " , Esc - выход, Space - пауза, для быстрого перехода между уровнями нажмите 1 для перехода на 1 уровень, 2 на второй, 3 на третий. Зелёный кубик - ускорение, жёлтый - замедление, фиолетовый - размер платформы, синий - появление нового шарика.";
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
