using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManagerNY : MonoBehaviour
{
    public Image bar; //이미지 컴포넌트 가져오기.
    public PlayerDeer player;

    NY_STATE gamestate;
    public enum NY_STATE
    {
        NONE = 0,
        INTRO,
        START,
        PLAY,
        GAMEOVER,
        GAEWINNING,
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
            case NY_STATE.GAEWINNING:
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

    }

    void GamePlay()
    {

    }

    void GameEnd()
    {

    }

    void GameWinning()
    {

    }

    void GameOver()
    {
        
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
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        // FillAmount 속성을 사용하여 이미지 업데이트
        bar.fillAmount = player.hp / player.maxHP;

     
    }

    // 체력을 감소시키는 함수
 
    public void checkWin()
    {
        if (player.isDie)
        {
            gamestate = NY_STATE.GAMEOVER;
        }

        if (player.isWin)
        {
            gamestate = NY_STATE.GAEWINNING;
        }
    }
}
