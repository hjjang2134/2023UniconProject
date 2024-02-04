using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeer : MonoBehaviour
{
    public float jump = 5.5f;
    public int jumpCount = 0;
    public bool isJump = false;
    public int speed = 7;
    public bool isDie = false; //die check
    public float hp = 100;
    public float maxHP = 100;
    public bool isWin = false; //win check
    public int score = 0;
    public float movePower = 10f;

    Vector3 movement;
    private Rigidbody2D rigid;


    /*public void Jump()
    {
        if (isDie != true)
        {
            if (!isJump)
            {
                isJump = true;
                rigid.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            }
        }
        
    }*/

    private void FixedUpdate()
    {
        //jumpCount <= 2
        if (rigid != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                rigid.velocity = Vector2.zero;
                rigid.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
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
        transform.localScale = new Vector3(-2.2f, 2.2f, 2.2f);
        transform.position = new Vector3(-8, -2, 0);

    }
    public void Move()
    {
        Vector3 moveVelocity = Vector3.right;
        
        transform.position += moveVelocity * movePower * Time.deltaTime;
        
        //gameObject.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);


        if (hp > 0 && !isDie)
            hp -= 0.006f;
        else
            isDie = true;

    }

    void Update()
    {
        //if(GameManagerNY.Instance.gamestate == GameManagerNY.NY_STATE.PLAY)
        Move();

    }
}

