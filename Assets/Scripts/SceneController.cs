using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject _brick;

    public static int countBall;

    public static int amount;

    public static string NameScene;

    private int timer = 1;

    private const int gridRows = 4;
    private const int gridCols = 10;
    private const float offsetX = 1.2f;
    private const float offsetY = 0.8f;

    public static float speedX;
    public static float speedY;

    // Start is called before the first frame update
    void Start()
    {
        NameScene = SceneManager.GetActiveScene().name;
        CreateBrick(NameScene);
        countBall = 1;
        Cursor.visible = false;
        speedX = 3.0f;
        speedY = 3.0f;
    }

    //Кол-во жизней блоков в зависимости от уровня.
    void CreateBrick(string _nameScene) 
    {
        switch (_nameScene) 
        {
            case "Level 1":
                {
                    Arrangement(1);
                    break;
                }
            case "Level 2" :
                {
                    Arrangement(3);
                    break;
                }
            case "Level 3":
                {
                    Arrangement(4);
                    break;
                }
        }
    }

    //Добавление блоков.
    void Arrangement(int _life) 
    {
        amount = gridCols * gridRows;
        Vector2 startPos = _brick.transform.position;
        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                GameObject brick;
                brick = Instantiate(_brick) as GameObject;
                brick.GetComponent<Brick>().life = Random.Range(1, _life);
                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;
                brick.transform.position = new Vector2(posX, posY);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Выход.
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Application.Quit();
        }

        //Пауза.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (timer == 1)
            {
                timer = 0;
                Time.timeScale = timer;
            }
            else
            {
                timer = 1;
                Time.timeScale = timer;
            }    
           
        }

        //Для быстрого перехода между уровнями.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("Level 1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("Level 2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene("Level 3");
        }
    }
}
