using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeer : MonoBehaviour
{
    public float jump = 5.2f;
    public int jumpCount = 0;
    public bool isJump = false;
    public float speed = 6.5f;
    public bool isDie = false; //die check
    public float hp = 100;
    public float maxHP = 100;
    public bool isWin = false; //win check
    public int score = 0;

    private Rigidbody2D rigid;


   private void FixedUpdate()
    {
        Move();
        Jump();
        
     
    }

    public void Jump()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (jumpCount < 2) // Jump only if jumpCount is less than 2
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, jump, 0);
                jumpCount++;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.CompareTo("Plane") == 0)
        {
            Debug.Log("plane check");
            //isJump = false;
            jumpCount = 0;
        }

        if (collision.gameObject.tag.CompareTo("Obstacle") == 0)
        {
            Debug.Log("obstacle check");
            hp -= 10;
        }

        if (collision.gameObject.tag.CompareTo("Success") == 0)
        {
            Debug.Log("success check");
            isWin = true;
            Debug.Log(isWin);
        }

       

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.CompareTo("DeadZone") == 0)
        {
            Debug.Log("die true");
            isDie = true;
            Debug.Log(isDie);
        }

        if(collision.gameObject.tag.CompareTo("Jelly") == 0)
        {
            Debug.Log("Jelly check");
            score += 10;
            collision.gameObject.SetActive(false);

        }
    }


    private void Start()
    {
        maxHP = 100;
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init()
    {
        isDie = false;
        isWin = false;
        hp = 100;
        score = 0;
        transform.position = new Vector3(-8, -2, 0);
        
    }

    public void Move()
    {
        if (GameManagerNY.Instance.gamestate == GameManagerNY.NY_STATE.PLAY)
        {
            gameObject.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);

            if (hp > 0 && !isDie)
                hp -= 0.006f;
            else
                isDie = true;

        }

    }

    void Update()
    {


    }
}

