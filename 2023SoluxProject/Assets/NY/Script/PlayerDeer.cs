using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeer : MonoBehaviour
{

    public float jump1 = 4.8f;
    public float jump2 = 5f;
    public int jumpCount = 0;
    public int speed = 7;
    public bool isDie = false; //die check
    public float hp = 100;
    public float maxHP = 100;
    public bool isWin = false; //win check
    public int score = 0;


    public void Jump()
    {
        if (isDie != true)
        {
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
            Debug.Log("plane check");
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

        if (collision.gameObject.tag.CompareTo("Jelly") == 0)
        {
            Debug.Log("Jelly check");
            score += 10;
            collision.gameObject.SetActive(false);

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
    }

    private void Start()
    {
        maxHP = 100; 
    }


    void Update()
    {
        
        if(!isWin && !isDie)
        {
            gameObject.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Jump();
        }

        if (hp > 0 && !isDie)
            hp -= 0.006f;
        else
            isDie = true;

    }
}

