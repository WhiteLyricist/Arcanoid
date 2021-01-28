using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class MoveBonus : MonoBehaviour
{

    [SerializeField] private GameObject _ball;

    private Vector2 InitialVector;

    private const float speedB = 3.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InitialVector = new Vector2(transform.position.x, transform.position.y - speedB * Time.deltaTime);
        transform.position = InitialVector;

        Collider[] _col = Physics.OverlapSphere(transform.position, 0.25f);
        foreach (var col in _col)
        {
            GameObject hit;
            hit = col.gameObject;
            if (hit.tag == "Platform") 
            {
                switch (this.tag)
                {
                    case "speedSlow":
                        {
                            if (SceneController.speedX > 0.5f && SceneController.speedY > 0.5f)
                            {
                                SceneController.speedX -= 0.5f;
                                SceneController.speedY -= 0.5f;
                                Debug.Log("Slow" + " " + SceneController.speedX + " " + SceneController.speedY);
                            }
                            break;
                        }
                    case "speedUp":
                        {
                            SceneController.speedX += 0.5f;
                            SceneController.speedY += 0.5f;
                            Debug.Log("Up" + " " + SceneController.speedX + " " + SceneController.speedY);
                            break;
                        }
                    case "doubling":
                        {
                            GameObject ball;
                            ball = Instantiate(_ball) as GameObject;
                            ball.transform.position = hit.transform.position;
                            SceneController.countBall += 1;
                            break;
                        }
                    case "expansion":
                        {
                            if (hit.transform.localScale.x < 6)
                            {
                                Vector2 width = new Vector2(hit.transform.localScale.x + 2, hit.transform.localScale.y);
                                hit.transform.localScale = width;
                            }
                            else
                            {
                                Vector2 width = new Vector2(hit.transform.localScale.x - 4, hit.transform.localScale.y);
                                hit.transform.localScale = width;
                            }
                            break;
                        }

                }
                Destroy(this.gameObject);
            }
        }
    }
}
