using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeer : MonoBehaviour
{
    public float jump1 = 5.5f;
    public float jump2 = 7f;
    public int jumpCount = 0;
    public int speed = 5;
    public bool isDie = false; //사망 판단 
    public float hp = 100;
    public float maxHP = 100;
    public bool isWin = false; //승리 판단

    public void Jump()
    {
        if (isDie != true)
        {
            //처음 점프할 때 
            if (jumpCount == 0)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, jump1, 0);
                jumpCount += 1;
            }
            else if (jumpCount == 1)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, jump2, 0);
                jumpCount = 0;
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.CompareTo("Plane") == 0)
        {
            Debug.Log("여기는 땅");
            jumpCount = 0;
        }

        if (collision.gameObject.tag.CompareTo("Obstacle") == 0)
        {
            Debug.Log("쓰레기랑 부딪힘");
            hp -= 7;
        }

        if (collision.gameObject.tag.CompareTo("Success") == 0)
        {
            Debug.Log("성공");
            isWin = true;
            Debug.Log(isWin);
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.CompareTo("DeadZone") == 0)
        {
            Debug.Log("떨어진다!");
            isDie = true;
            Debug.Log(isDie);
        }
    }

    private void Start()
    {
        maxHP = 100; //최대 체력 초기화
    }


    void Update()
    {
        
        if(!isWin && !isDie)
        {
            //앞으로 나아가고, jump
            gameObject.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Jump();
            Debug.Log("jump check");
        }

        if (hp > 0 && !isDie)
            hp -= 0.006f;
        else
            isDie = true;

    }
}

