using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //���� �����
    public int lives = 3;

    //��������
    public int bricks = 32;

    //���� ����� �ð�
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

    //���� ���۽� �е�� ������ �ҷ��´�
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


    //���� ����� ����
    void CheckGamOver()
    {
        //������ ���� ��
        if (bricks < 1)
        {
            if (success != null)
            {
                success.SetActive(true);
                //�ð��� 2.5�� ������
                Time.timeScale = 2.5f;
                Invoke("Reset", resetDelay);
            }
        }

        //������� ����������
        if (lives < 1)
        {
            if (gameOver != null)
            {
                gameOver.SetActive(true);
                //�ð��� 0.25�� ������ 
                Time.timeScale = 0.25f;
                Invoke("Reset", resetDelay);
            }
        }

    }

    private void Reset()
    {
        //���Ÿ���� �����մϴ�
        Time.timeScale = 1f;

        Application.LoadLevel(Application.loadedLevel);
    }

    //������� �ҰԵǸ� �߻�
    public void LoseLife()
    {
        lives--;

        if (txtLives != null)
        {
            txtLives.text = "���� ���� : " + lives;
        }

        //��ƼŬ �߻�
        if (DeathParticles != null)
        {
            Instantiate(DeathParticles, clonePaddle.transform.position, Quaternion.identity);
        }

        //�е� ���ֱ�
        Destroy(clonePaddle.gameObject);

        //������ �ð� ��ŭ ������ �е� ����
        Invoke("SetupPaddle", resetDelay);
        CheckGamOver();
    }

    //�е� ���� 
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