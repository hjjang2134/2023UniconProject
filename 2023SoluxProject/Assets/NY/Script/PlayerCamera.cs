using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform player;  // 플레이어의 Transform을 저장할 변수
    public float smoothSpeed = 0.125f;  // 카메라 이동을 부드럽게 하기 위한 스무딩 계수

    void LateUpdate()
    {
        if (player != null)
        {
            // 플레이어의 현재 X 위치를 가져오기
            float targetX = player.position.x;

            // 현재 카메라의 위치와 플레이어의 X 위치를 보간하여 부드럽게 이동
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, new Vector3(targetX, transform.position.y, transform.position.z), smoothSpeed);

            // 카메라 위치를 업데이트
            transform.position = smoothedPosition;
        }
    }
}
