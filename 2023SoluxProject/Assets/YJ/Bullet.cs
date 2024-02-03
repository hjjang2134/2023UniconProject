using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Paddle paddle;

    void OnEnable()
    {
        // 처음 총알의 위치를 정해 준다.
        transform.position = new Vector2(paddle.paddleX + (CompareTag("Odd") ? -0.904f : 0.904f), -2.867949f);
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * 0.05f);
        Invoke("ActiveFalse", 2); // delay를 줘서 함수를 실행시킴
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.name)
        {
            case "Block":
            case "HardBlock0":
            case "HardBlock1":
            case "HardBlock2":
                GameObject Col = col.gameObject;
                paddle.BlockBreak(Col, Col.transform, Col.GetComponent<Animator>());
                ActiveFalse();
                break;
            case "Background":
                ActiveFalse();
                break;
        }
    }

    void ActiveFalse() { gameObject.SetActive(false); }

}
