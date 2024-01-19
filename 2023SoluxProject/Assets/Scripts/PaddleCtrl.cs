using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleCtrl : MonoBehaviour
{
    public float PaddleSpeed = 1f; // Paddle �̵� ���ǵ�
    private Vector3 playerPos = new Vector3(0f, -3.5f, 0f); // Paddle�� �ʱ� ��ġ

    // Update is called once per frame
    private void Update()
    {
        // Paddle �¿� �̵�
        float xPos = transform.position.x + (Input.GetAxis("Horizontal")*PaddleSpeed);

        // Paddle�� �̵� ������ ��
        playerPos = new Vector3(Mathf.Clamp(xPos, -8f, 8f), -3.5f, 0f);
        transform.position = playerPos;
    }
}
