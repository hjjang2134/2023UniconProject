using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
   public void Retry()
    {
        // 무한 루프를 방지하기 위한 최대 시도 횟수
        const int maxTries = 1000;
        int currentTry = 0;

        do
        {
            // 초기 보드 생성
            Board.Instance.CreateInitialBoard();

            // 세 개 이상의 연속된 버블이 없는지 확인
            if (!Board.Instance.HasConsecutiveBubbles())
            {
                // 조건을 만족할 때까지 다시 시도
                currentTry++;
            }
            else
            {
                // 조건을 만족하면 루프 종료
                break;
            }
        } while (currentTry < maxTries);

        //스코어 초기화 
        Score.Instance.ResetScore();

    }
}
