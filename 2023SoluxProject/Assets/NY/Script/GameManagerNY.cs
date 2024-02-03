using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManagerNY : MonoBehaviour
{
    public Image bar; //이미지 컴포넌트 가져오기.
    public PlayerDeer player;

    //ui
    public GameObject g_ui_Success;
    public GameObject g_ui_GameOver;

    NY_STATE gamestate;
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
                GameIntro();
                break;
            case NY_STATE.START:
                gamestate = NY_STATE.NONE;
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

    void GameIntro()
    {
        g_ui_Success.SetActive(false);
        g_ui_GameOver.SetActive(false);
    }

    void GamePlay()
    {

    }

    void GameEnd()
    {

    }

    void GameWinning()
    {
        g_ui_Success.SetActive(true);
    }

    void GameOver()
    {
        g_ui_GameOver.SetActive(true);
    }

    public void GameRestart()
    {
        gamestate = NY_STATE.START;
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        if(gamestate != NY_STATE.GAMEWINNING || gamestate != NY_STATE.GAMEOVER)
            UpdateHealthBar();

        checkWin();
        checkGameOver();
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
        GameState();
        if (player.isDie == true)
        {
            gamestate = NY_STATE.GAMEOVER;
            Debug.Log("ui_lose");
        }
    }
}
