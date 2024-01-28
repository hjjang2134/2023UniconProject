using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeer : MonoBehaviour
{

    public float jump1 = 5.5f;
    public float jump2 = 7f;
    public int jumpCount = 0;
    public int speed = 5;
    public bool isDie = false; //��� �Ǵ� 
    public float hp = 100;
    public float maxHP = 100;
    public bool isWin = false; //�¸� �Ǵ�


    public void Jump()
    {
        if (isDie != true)
        {
            //ó�� ������ �� 
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
            Debug.Log("����� ��");
            jumpCount = 0;
        }

        if (collision.gameObject.tag.CompareTo("Obstacle") == 0)
        {
            Debug.Log("������� �ε���");
            hp -= 7;
        }

        if (collision.gameObject.tag.CompareTo("Success") == 0)
        {
            Debug.Log("����");
            isWin = true;
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.CompareTo("DeadZone") == 0)
        {
            Debug.Log("��������!");
            isDie = true;
        }
    }

    private void Start()
    {
        maxHP = 100; //�ִ� ü�� �ʱ�ȭ
    }


    void Update()
    {
        if(!isWin && !isDie)
        {
            //������ ���ư���, jump
            gameObject.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Jump();
        }

        hp -= 0.005f;

    }
}

