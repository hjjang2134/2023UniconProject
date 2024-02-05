using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManagerNY : MonoBehaviour
{
    public static GameManagerNY Instance;

    public Image bar; //이미지 컴포넌트 가져오기.
    public PlayerDeer player;

    public string jellyTag = "Jelly";

    //ui
    public GameObject g_ui_Start;
    public GameObject g_ui_SuccessText;
    public GameObject g_ui_GameOver;
    public Text ui_score;

    //audio
    public AudioSource S_start;
    public AudioSource S_wowwow;
    public AudioSource S_bgm;
    public AudioSource S_GameOver;

    public NY_STATE gamestate;

    public enum NY_STATE
    {
        NONE = 0,
        INTRO,
        START,
        PLAY,
        GAMEOVER,
        GAMEWINNING,
        END
    }

    void GameState()
    {
        switch (gamestate)
        {
            case NY_STATE.NONE:
                break;
            case NY_STATE.INTRO:
                gamestate = NY_STATE.NONE;
                StartCoroutine(GameIntro());
                break;
            case NY_STATE.START:
                gamestate = NY_STATE.NONE;
                GameStart();
                break;
            case NY_STATE.PLAY:
                GamePlay();
                break;
            case NY_STATE.GAMEOVER:
                gamestate = NY_STATE.NONE;
                GameOver();
                break;
            case NY_STATE.GAMEWINNING:
                gamestate = NY_STATE.NONE;
                GameWinning();
                break;
            case NY_STATE.END:
                gamestate = NY_STATE.NONE;
                GameEnd();
                break;
        }
    }

    IEnumerator GameIntro()
    {
        g_ui_Start.SetActive(true);
        g_ui_GameOver.SetActive(false);
        S_start.Play();
        Debug.Log(gamestate);
        yield return new WaitForSeconds(3.0f);

        g_ui_Start.SetActive(false);
        S_bgm.Play();
        gamestate = NY_STATE.START;
    }

    void GameStart()
    {
        player.Init();
        gamestate = NY_STATE.PLAY;
    }


    void GamePlay()
    {
        
        Debug.Log(gamestate);
        checkWin();
        checkGameOver();
        UpdateHealthBar();
        
    }

    void GameEnd()
    {

    }

    void GameWinning()
    {
        g_ui_SuccessText.SetActive(true);
        gamestate = NY_STATE.NONE;
        S_bgm.Stop();
        S_wowwow.Play();
    }

    void GameOver()
    {
        g_ui_GameOver.SetActive(true);
        S_bgm.Stop();
        S_GameOver.Play();
    }

    public void GameRestart()
    {
        g_ui_GameOver.SetActive(false);
        gamestate = NY_STATE.START;
        ActivateAllJellyObjects();
    }

    void Start()
    {
        UpdateHealthBar();
        gamestate = NY_STATE.INTRO;
        Instance = this;

    }
    
    void Update()
    {
        GameState();
        ui_score.text = player.score.ToString();

    }

    void UpdateHealthBar()
    {
        // FillAmount 속성을 사용하여 이미지 업데이트
        bar.fillAmount = player.hp / player.maxHP;

    }

 
    public void checkWin()
    {
        if (player.isWin == true)
        {
            gamestate = NY_STATE.GAMEWINNING;
            Debug.Log("ui_win");
        }

    }

    public void checkGameOver()
    {
        if (player.isDie == true)
        {
            gamestate = NY_STATE.GAMEOVER;
            Debug.Log("ui_lose");
        }
    }

    void ActivateAllJellyObjects() //jelly reSetActive
    {
        for(int i = 1; i <= player.score / 10; i++)
        {
            GameObject.Find("JellyParent").transform.GetChild(i).gameObject.SetActive(true);
        }
    }


}
