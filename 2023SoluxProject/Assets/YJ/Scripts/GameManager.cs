using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //남은 생명력
    public int lives = 3;

    //벽돌갯수
    public int bricks = 32;

    //게임 재시작 시간
    public float resetDelay;


    public TextMeshProUGUI txtLives = null;
    public GameObject gameOver = null;
    public GameObject success = null;
    public GameObject bricksPrefab;
    public GameObject paddle = null;
    public GameObject DeathParticles = null;
    public static GameManager Instance = null;

    private GameObject clonePaddle = null;


    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        SetUp();
    }

    //게임 시작시 패들과 벽돌을 불러온다
    public void SetUp()
    {
        if (paddle != null)
        {
            clonePaddle = Instantiate(paddle, paddle.transform.position, Quaternion.identity) as GameObject;
        }

        if (bricksPrefab != null)
        {
            Instantiate(bricksPrefab, bricksPrefab.transform.position, Quaternion.identity);
        }
    }


    //게임 재시작 설정
    void CheckGamOver()
    {
        //벽돌을 깻을 때
        if (bricks < 1)
        {
            if (success != null)
            {
                success.SetActive(true);
                //시간을 2.5배 빠르게
                Time.timeScale = 2.5f;
                Invoke("Reset", resetDelay);
            }
        }

        //생명력을 소진했을때
        if (lives < 1)
        {
            if (gameOver != null)
            {
                gameOver.SetActive(true);
                //시간을 0.25배 느리게 
                Time.timeScale = 0.25f;
                Invoke("Reset", resetDelay);
            }
        }

    }

    private void Reset()
    {
        //평균타임을 설정합니다
        Time.timeScale = 1f;

        Application.LoadLevel(Application.loadedLevel);
    }

    //생명력을 잃게되면 발생
    public void LoseLife()
    {
        lives--;

        if (txtLives != null)
        {
            txtLives.text = "남은 생명 : " + lives;
        }

        //파티클 발생
        if (DeathParticles != null)
        {
            Instantiate(DeathParticles, clonePaddle.transform.position, Quaternion.identity);
        }

        //패들 없애기
        Destroy(clonePaddle.gameObject);

        //딜레이 시간 만큼 지나면 패들 생산
        Invoke("SetupPaddle", resetDelay);
        CheckGamOver();
    }

    //패들 생산 
    private void SetupPaddle()
    {
        clonePaddle = Instantiate(paddle, transform.position, Quaternion.identity) as GameObject;
    }

    public void DestroyBrick()
    {
        bricks--;
        CheckGamOver();
    }

}