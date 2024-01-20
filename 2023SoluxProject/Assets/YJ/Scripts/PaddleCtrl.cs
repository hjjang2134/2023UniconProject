using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleCtrl : MonoBehaviour
{
    public float PaddleSpeed = 1f; // Paddle 이동 스피드
    private Vector3 playerPos = new Vector3(0f, -3.5f, 0f); // Paddle의 초기 위치

    // Update is called once per frame
    private void Update()
    {
        // Paddle 좌우 이동
        float xPos = transform.position.x + (Input.GetAxis("Horizontal")*PaddleSpeed);

        // Paddle에 이동 제한을 줌
        playerPos = new Vector3(Mathf.Clamp(xPos, -8f, 8f), -3.5f, 0f);
        transform.position = playerPos;
    }
}
