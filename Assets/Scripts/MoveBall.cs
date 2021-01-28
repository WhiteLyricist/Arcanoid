using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MoveBall : MonoBehaviour
{

    [SerializeField] private GameObject _speedUp;
    [SerializeField] private GameObject _speedSlow;
    [SerializeField] private GameObject _doubling;
    [SerializeField] private GameObject _expansion;

    private Vector2 InitialVector;

    private float deltaX;
    private float deltaY;

    GameObject hit;


    // Start is called before the first frame update
    void Start()
    {
        deltaX = Random.Range(-1.0f, 1.0f); 
        deltaY = Random.Range(.0f, 1.0f); 
    }

    // Update is called once per frame
    void Update()
    {
        InitialVector = new Vector2(transform.position.x + deltaX * SceneController.speedX * Time.deltaTime / Mathf.Abs(deltaX), transform.position.y + deltaY * SceneController.speedY * Time.deltaTime / Mathf.Abs(deltaY));
        transform.position = InitialVector;

        //Обработка попадания шара в объект.
        Collider[] _col = Physics.OverlapSphere(transform.position,0.25f);
        foreach (var col in _col) 
        {
            hit = col.gameObject;

            //Расстояние от центра шара до центра объекта в который он попал.
            float dist = Vector2.Distance(transform.position, hit.transform.position);
            //Расстояние от центра объекта в который попал шар, до шара если бы он попал в угол.
            float hyp = Mathf.Pow(hit.transform.localScale.x / 2 + transform.localScale.x / 2, 2) + Mathf.Pow(hit.transform.localScale.y / 2 + transform.localScale.y / 2, 2);

            //Попаданияе в угол, в каждый угол можно попасть с 3-х различных сторон.
            if (Mathf.Round(Mathf.Pow(dist,2)) == Mathf.Round(hyp) && (hit.tag=="Brick" || hit.tag == "Platform"))
            {
                //12
                if (deltaX > 0 && deltaY > 0 && transform.position.y > hit.transform.position.y) 
                {
                    deltaX = -deltaX;
                    goto Next;
                }
                //4
                if (deltaX > 0 && deltaY > 0 && transform.position.y < hit.transform.position.y && transform.position.x > hit.transform.position.x)
                {
                    deltaY = -deltaY;
                    goto Next;
                }
                //2
                if (deltaX > 0 && deltaY > 0 && transform.position.y < hit.transform.position.y)
                {
                    deltaX = -deltaX;
                    deltaY = -deltaY;
                    goto Next;
                }
                //9
                if (deltaX > 0 && deltaY < 0 && transform.position.y > hit.transform.position.y && transform.position.x > hit.transform.position.x)
                {
                    deltaY = -deltaY;
                    goto Next;
                }
                //11
                if (deltaX > 0 && deltaY < 0 && transform.position.y > hit.transform.position.y)
                {
                    deltaX = -deltaX;
                    deltaY = -deltaY;
                    goto Next;
                }
                //1
                if (deltaX > 0 && deltaY < 0 && transform.position.y < hit.transform.position.y)
                {
                    deltaX = -deltaX;
                    goto Next;
                }
                //7
                if (deltaX < 0 && deltaY > 0 && transform.position.y > hit.transform.position.y)
                {
                    deltaX = -deltaX;
                    goto Next;
                }
                //3
                if (deltaX < 0 && deltaY > 0 && transform.position.y < hit.transform.position.y && transform.position.x < hit.transform.position.x)
                {
                    deltaY = -deltaY;
                    goto Next;
                }
                //5
                if (deltaX < 0 && deltaY > 0 && transform.position.y < hit.transform.position.y)
                {
                    deltaX = -deltaX;
                    deltaY = -deltaY;
                    goto Next;
                }
                //10
                if (deltaX < 0 && deltaY < 0 && transform.position.y > hit.transform.position.y && transform.position.x < hit.transform.position.x)
                {
                    deltaY = -deltaY;
                    goto Next;
                }
                //8
                if (deltaX < 0 && deltaY < 0 && transform.position.y > hit.transform.position.y)
                {
                    deltaX = -deltaX;
                    deltaY = -deltaY;
                    goto Next;
                }
                //6
                if (deltaX < 0 && deltaY < 0 && transform.position.y < hit.transform.position.y)
                {
                    deltaX = -deltaX;
                    goto Next;
                }
            }

            if (hit.tag != "speedSlow" && hit.tag != "speedUp" && hit.tag != "doubling" && hit.tag != "expansion")
            {
                //Попадание в крышку.
                if (deltaY < 0 && transform.position.y > hit.transform.position.y && (hit.transform.position.x - hit.transform.localScale.x / 2 < transform.position.x) && (transform.position.x < hit.transform.position.x + hit.transform.localScale.x / 2))
                {
                    deltaY = -deltaY;
                    goto Next;
                }
                //Попадание в дно.
                if (deltaY > 0 && transform.position.y < hit.transform.position.y && (hit.transform.position.x - hit.transform.localScale.x / 2 < transform.position.x) && (transform.position.x < hit.transform.position.x + hit.transform.localScale.x / 2))
                {
                    deltaY = -deltaY;
                    goto Next;
                }
                //Попадание в левую сторону.
                if (deltaX > 0 && transform.position.x < hit.transform.position.x && (hit.transform.position.y - hit.transform.localScale.y / 2 < transform.position.y) && (transform.position.y < hit.transform.position.y + hit.transform.localScale.y / 2))
                {
                    deltaX = -deltaX;
                    goto Next;
                }
                //Попадание в правую сторону.
                if (deltaX < 0 && transform.position.x > hit.transform.position.x && (hit.transform.position.y - hit.transform.localScale.y / 2 < transform.position.y) && (transform.position.y < hit.transform.position.y + hit.transform.localScale.y / 2))
                {
                    deltaX = -deltaX;
                    goto Next;
                }
            }
        Next:

            if (hit.tag == "Brick")
            {
                DestroyBrick(SceneController.NameScene);
            }
            //Уровень начинается заново если последний шарик падает вниз.    
            if (hit.tag == "Floor") 
            {
                SceneController.countBall -= 1;
                if (SceneController.countBall==0) 
                {
                    SceneManager.LoadScene(SceneController.NameScene);
                }
                Destroy(this.gameObject);
            }
        }
    }

    private void DestroyBrick(string _nameScene)
    {
        //Разное поведение блоков при попадании в них шарика в зависимсоти от уровня.
        switch (_nameScene)
        {
            case "Level 1":
                {
                    Destroy(hit);
                    SceneController.amount -= 1;
                    if (SceneController.amount == 0)
                    {
                        SceneManager.LoadScene("Level 2");
                    }
                break;
                }
            case "Level 2":
                {
                    if (hit.GetComponent<Brick>().life == 1)
                    {
                        Destroy(hit);
                        SceneController.amount -= 1;
                        if (SceneController.amount == 0)
                        {
                            SceneManager.LoadScene("Level 3");
                        }
                    }
                    else { hit.GetComponent<Brick>().life -= 1; }
                    break;
                }
            case "Level 3":
                {
                    if (hit.GetComponent<Brick>().life == 1)
                    {
                        int _idBonus;
                        _idBonus = Random.Range(0, 5);
                        switch (_idBonus) 
                        {
                            //Без бонусов.
                            case 0 :
                                {
                                    Destroy(hit);
                                    break;
                                }
                            //Замедление шарика.
                            case 1: 
                                {
                                    GameObject bonus;
                                    bonus = Instantiate(_speedSlow) as GameObject;
                                    bonus.transform.position = hit.transform.position;
                                    Destroy(hit);
                                    break;
                                }
                            //Ускорение шарика.
                            case 2:
                                {
                                    GameObject bonus;
                                    bonus = Instantiate(_speedUp) as GameObject;
                                    bonus.transform.position = hit.transform.position;
                                    Destroy(hit);
                                    break;
                                }
                            //Добавление шарика.
                            case 3:
                                {
                                    GameObject bonus;
                                    bonus = Instantiate(_doubling) as GameObject;
                                    bonus.transform.position = hit.transform.position;
                                    Destroy(hit);
                                    break;
                                }
                            //Увеличение платформы.
                            case 4:
                                {
                                    GameObject bonus;
                                    bonus = Instantiate(_expansion) as GameObject;
                                    bonus.transform.position = hit.transform.position;
                                    Destroy(hit);
                                    break;
                                }
                        }

                        SceneController.amount -= 1;
                        if (SceneController.amount == 0)
                        {
                            Application.Quit(); 
                        }
                    }
                    else { hit.GetComponent<Brick>().life -= 1; }
                    break;
                }
        }
    }

}
