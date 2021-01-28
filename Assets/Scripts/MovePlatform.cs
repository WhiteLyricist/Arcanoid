using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{

    public float speed = 10.0f;

    private GameObject WallL;
    private GameObject WallR;

    // Start is called before the first frame update
    void Start()
    {
       WallL = GameObject.Find("Wall L");
       WallR = GameObject.Find("Wall R");
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        Vector2 movement = new Vector2(transform.position.x+deltaX, transform.position.y);
        //Ограничение перемещения платформы.
        if ((transform.position.x - transform.localScale.x / 2) > (WallL.transform.position.x + WallR.transform.localScale.x / 2) && Input.GetAxis("Horizontal")<0)
        { 
            transform.position = movement;
        }
        if ((transform.position.x + transform.localScale.x / 2) < (WallR.transform.position.x - WallR.transform.localScale.x / 2) && Input.GetAxis("Horizontal") > 0)
        {
            transform.position = movement;
        }
    }
}
