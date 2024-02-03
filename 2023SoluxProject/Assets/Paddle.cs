// 고칠 것 목록

// 자꾸 오류 나는 애니메이션 수정


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Paddle : MonoBehaviour
{
    [Multiline(12)]
    public string[] StageStr;
    public Sprite[] B;
    public GameObject P_Item;
    public SpriteRenderer P_ItemSr;
    public TextMeshProUGUI StageText;
    public TextMeshProUGUI ScoreText;
    public GameObject Life0;
    public GameObject Life1;
    public GameObject Life2;
    public GameObject WinPanel;
    public GameObject GameOverPanel;
    public GameObject ComboPanel; //콤보 추가
    public GameObject[] Combocol; //콤보 추가
    public int comboBlockIndex = -1; // 콤보 인뎃스
    public GameObject PausePanel;
    public GameObject yj_EndingPanel; // 엔딩패널
    public AudioSource S_Break;
    public AudioSource S_Eat;
    public AudioSource S_Fail;
    public AudioSource S_Gun;
    public AudioSource S_HardBreak;
    public AudioSource S_Paddle;
    public AudioSource S_Victory;
    public AudioSource S_Combo;// 콤보 소리
    public Transform ItemsTr;
    public Transform BlocksTr;
    public BoxCollider2D[] BlockCol;
    public GameObject[] Ball;
    public Animator[] BallAni;
    public Transform[] BallTr;
    public SpriteRenderer[] BallSr;
    public Rigidbody2D[] BallRg;
    public GameObject[] Bullet;
    public SpriteRenderer PaddleSr;
    public BoxCollider2D PaddleCol;
    public GameObject Magnet;
    public GameObject Gun;

    bool isStart;
    public float paddleX;
    public float ballSpeed;
    float oldBallSpeed = 300;
    float paddleBorder = 9.5f;    //바꿔줘야돼
    float paddleSize = 1.58f;
    int combo;
    int score;
    int stage;

#if (UNITY_ANDROID)
        void Awake() { Screen.SetResolution(1170, 540, false); }
#else
    void Awake() { Screen.SetResolution(2340, 1080, false); }
#endif
    // 뒤로가기 키 누르면 일시정지
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PausePanel.activeSelf) { PausePanel.SetActive(false); Time.timeScale = 1; }
            else { PausePanel.SetActive(true); Time.timeScale = 0; }
        }
    }

    // 스테이지 초기화 (-1 재시작, 0 다음 스테이지, 숫자 스테이지)
    public void AllReset(int _stage)
    {
        Debug.Log("_stage : " +_stage + "/ stage : " + stage);
        if (_stage == 0) stage++;
        else if(_stage != -1) stage = _stage;
        

        // 스테이지가 4가 되면 구출 성공 패널 뜸
        if (stage >= (StageStr.Length)) {
            if (!yj_EndingPanel.activeSelf)
            {
                yj_EndingPanel.SetActive(true);
                
            }
            
            return;
        }

        Clear();
        BlockGenerator();
        StartCoroutine("BallReset");

        // 점수 초기화
        StageText.text = stage.ToString();
        /*score = 0;
        ScoreText.text = "0";*/

        PaddleSr.enabled = true;
        Life0.SetActive(true);
        Life1.SetActive(true);
        Life2.SetActive(true);
        WinPanel.SetActive(false);
        GameOverPanel.SetActive(false);

        

    }

    // 블럭 생성
    // 근데 커스텀할 필요 없을 거 같아서 나중에 지울수도 이떠.. 
    void BlockGenerator()
    {
        string currentStr = StageStr[stage].Replace("\n", ""); // 현재 스테이지를 불러옴
        currentStr = currentStr.Replace(" ", ""); // 띄어쓰기가 있으면 처리함
        for (int i = 0; i < currentStr.Length; i++)
        {
            BlockCol[i].gameObject.SetActive(false); // 블록을 순번대로 불러옴 
            char A = currentStr[i]; string currentName = "Block"; int currentB = 0;

            if (A == '*') continue;
            else if (A == '8') { currentB = 8; currentName = "HardBlock0"; }
            else if (A == '9') currentB = Random.Range(0, 8);
            else currentB = int.Parse(A.ToString());

            BlockCol[i].gameObject.name = currentName;
            BlockCol[i].gameObject.GetComponent<SpriteRenderer>().sprite = B[currentB];
            BlockCol[i].gameObject.SetActive(true);
        }
    }

    // 볼 위치 초기화하고 0.7초간 깜빡이는 애니메이션 재생
    IEnumerator BallReset()
    {
        isStart = false;
        combo = 0;
        Ball[0].SetActive(true);
        Ball[1].SetActive(false);
        Ball[2].SetActive(false);
        // Ball[3].SetActive(false);
        BallAni[0].SetTrigger("Blink");
        BallTr[0].position = new Vector2(paddleX, -2.87f);

        StopCoroutine("InfinityLoop");
        yield return new WaitForSeconds(0.7f);
        StartCoroutine("InfinityLoop");
    }

    IEnumerator InfinityLoop()
    {
        while (true)
        {
            if(Input.GetMouseButton(0) || (Input.touchCount==1 && Input.GetTouch(0).phase == TouchPhase.Moved))
            {
                paddleX = Mathf.Clamp(Camera.main.ScreenToWorldPoint(Input.GetMouseButton(0) ? Input.mousePosition : (Vector3)Input.GetTouch(0).position).x, -paddleBorder, paddleBorder);
                transform.position = new Vector2(paddleX, transform.position.y);
                if(!isStart) BallTr[0].position = new Vector2(paddleX, BallTr[0].position.y);
            }

            if(!isStart && (Input.GetMouseButtonUp(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)))
            {
                isStart = true;
                ballSpeed = oldBallSpeed;
                BallRg[0].AddForce(new Vector2(0.1f, 0.9f).normalized*ballSpeed);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
    
    // 볼이 충돌할 때
    public IEnumerator BallCollisionEnter2D(Transform ThisBallTr, Rigidbody2D ThisBallRg, Ball ThisBallCs, GameObject Col, Transform ColTr, SpriteRenderer ColSr, Animator ColAni)
    {
        Physics2D.IgnoreLayerCollision(2, 2);
        if (!isStart) yield break;

        switch (Col.name)
        {
            // 패들에 부딪히면 차이값만큼 힘 줌
            case "Paddle":
                ThisBallRg.velocity = Vector2.zero;
                ThisBallRg.AddForce((ThisBallTr.position - transform.position).normalized * ballSpeed);
                S_Paddle.Play();
                combo = 0;
                break;

            // 자석패들에 부딪히면 볼이 자석에 붙어있음
            /*
            case "MagnetPaddle":
                ThisBallCs.isMagnet = true;
                ThisBallRg.velocity = Vector2.zero;
                float gapX = transform.position.x - ThisBallTr.position.x;
                while (ThisBallCs.isMagnet)
                {
                    if (Input.GetMouseButton(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved))
                        ThisBallTr.position = new Vector2(transform.position.x + gapX, ThisBallTr.position.y);

                    if (gameObject.name == "Paddle" || (Input.GetMouseButtonUp(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)))
                    {
                        ThisBallRg.velocity = Vector2.zero;
                        ThisBallRg.AddForce((ThisBallTr.position - transform.position).normalized * ballSpeed);
                        ThisBallCs.isMagnet = false;
                    }
                    yield return new WaitForSeconds(0.01f);
                }
                break;
            */

            case "DeathZone":
                ThisBallTr.gameObject.SetActive(false);
                BallCheck(); // 볼체크
                break;

            // 돌0에 부딪히면 돌1이 됨
            case "HardBlock0":
                Col.name = "HardBlock1";
                ColSr.sprite = B[9];
                S_HardBreak.Play();
                break;

            // 돌1에 부딪히면 돌2이 됨
            case "HardBlock1":
                Col.name = "HardBlock2";
                ColSr.sprite = B[10];
                S_HardBreak.Play();
                break;

            // 블럭이나 돌에 부딪히면 부숴짐
            case "HardBlock2":
            case "Block":
                BlockBreak(Col, ColTr, ColAni);
                break;
        }
    }

    // 패들이 아이템과 충돌할 때
    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(col.gameObject);
        S_Eat.Play();
        switch (col.name)
        {
            // 볼 3개 전부 활성화
            case "Item_TripleBall":
                GameObject OneBall = BallCheck();
                for (int i = 0; i < 3; i++)
                {
                    if (OneBall.name == Ball[i].name) continue;
                    BallTr[i].position = OneBall.transform.position;
                    Ball[i].SetActive(true);
                    BallRg[i].velocity = Vector2.zero;
                    BallRg[i].AddForce(Random.insideUnitCircle.normalized * ballSpeed);
                }
                break;

            // 7.5초동안 패들이 커짐
            case "Item_Big":
                paddleSize = 2.42f;
                paddleBorder = 9.5f; // 바꿔줘야됨
                StopCoroutine("Item_BigOrSmall");
                StartCoroutine("Item_BigOrSmall", false);
                break;

            // 7.5초동안 패들이 작아짐
            case "Item_Small":
                paddleSize = 0.82f;
                paddleBorder = 10f;  // 바꿔줘야됨
                StopCoroutine("Item_BigOrSmall");
                StartCoroutine("Item_BigOrSmall", false);
                break;

        /*
            // 7.5초동안 볼의 속도가 느려짐(노잼이라 지울수도) 
            case "Item_SlowBall":
                StopCoroutine("Item_SlowBall");
                StartCoroutine("Item_SlowBall", false);
                break;

            // 4초동안 불공이 됨(노잼이라 지울수도) 
            case "Item_FireBall":
                StopCoroutine("Item_FireBall");
                StartCoroutine("Item_FireBall", false);
                break;

            // 7.5초동안 자석 활성화
            case "Item_Magnet":
                StopCoroutine("Item_Magnet");
                StartCoroutine("Item_Magnet", false);
                break;
            */

            // 4초동안 24발의 총알을 발사함
            case "Item_Gun":
                StopCoroutine("Item_Gun");
                StartCoroutine("Item_Gun", false);
                break;
        }
    }

    IEnumerator Item_BigOrSmall(bool skip)
    {
        if (!skip)
        {
            PaddleSr.size = new Vector2(paddleSize, PaddleSr.size.y);
            PaddleCol.size = new Vector2(paddleSize, PaddleCol.size.y);
            yield return new WaitForSeconds(7.5f);
        }
        paddleSize = 1.58f;
        paddleBorder = 9.5f;
        PaddleSr.size = new Vector2(paddleSize, PaddleSr.size.y);
        PaddleCol.size = new Vector2(paddleSize, PaddleCol.size.y);
    }

    /*
    IEnumerator Item_SlowBall(bool skip) // 얜 노잼이라 안나와도 될수있음 
    {
        if (!skip)
        {
            for (int i = 0; i < 3; i++)
            {
                ballSpeed = 250;
                BallAddForce(BallRg[i]);
            }
            yield return new WaitForSeconds(7.5f);
        }
        for (int i = 0; i < 3; i++)
        {
            ballSpeed = oldBallSpeed;
            BallAddForce(BallRg[i]);
        }
    }
    
    //불공 아이템 실행 근데 노잼이라 지울수도 있음 
    IEnumerator Item_FireBall(bool skip)
    {
        if (!skip)
        {
            for (int i = 0; i < 3; i++)
            {
                BallSr[i].sprite = B[23];
                ParticleSystem.MainModule PS = BallTr[i].GetChild(0).GetComponent<ParticleSystem>().main;
                PS.startColor = Color.red;
            }
            for (int i = 0; i < BlockCol.Length; i++)
            {
                BlockCol[i].tag = "TriggerBlock";
                BlockCol[i].isTrigger = true;
            }
            yield return new WaitForSeconds(4);
        }
        for (int i = 0; i < 3; i++)
        {
            BallSr[i].sprite = B[22];
            ParticleSystem.MainModule PS = BallTr[i].GetChild(0).GetComponent<ParticleSystem>().main;
            PS.startColor = Color.white;
        }
        for (int i = 0; i < BlockCol.Length; i++)
        {
            BlockCol[i].tag = "Untagged";
            BlockCol[i].isTrigger = false;
        }
    }

    // 자석 아이템 설정
    IEnumerator Item_Magnet(bool skip)
    {
        if (!skip)
        {
            gameObject.name = "MagnetPaddle";
            Magnet.SetActive(true);
            yield return new WaitForSeconds(5.5f);
            Magnet.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            Magnet.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            Magnet.SetActive(false);
            yield return new WaitForSeconds(0.25f);
            Magnet.SetActive(true);
            yield return new WaitForSeconds(0.25f);
            Magnet.SetActive(false);
            yield return new WaitForSeconds(0.25f);
            Magnet.SetActive(true);
            yield return new WaitForSeconds(0.25f);
        }
        gameObject.name = "Paddle";
        Magnet.SetActive(false);
    }
    */

    IEnumerator Item_Gun(bool skip)
    {
        if (!skip)
        {
            Gun.SetActive(true);
            for (int i = 0; i < 12; i++)
            {
                Bullet[i * 2].SetActive(true);
                Bullet[i * 2 + 1].SetActive(true);
                S_Gun.Play();
                yield return new WaitForSeconds(0.34f);
            }
        }
        Gun.SetActive(false);
    }

    public void BlockBreak(GameObject Col, Transform ColTr, Animator ColAni)
    {
        // 아이템 생성
        ItemGenerator(ColTr.position);

        // 추가한 것
        comboBlockIndex = -1;
        for (int i = 0; i < BlocksTr.childCount; i++)
        { 
            if(BlocksTr.GetChild(i).gameObject == Col)
            {
                comboBlockIndex = i;
            }        
        }

       // 스코어 증가, 콤보당 1점, 3콤보이상은 3점
        score += (++combo > 3) ? 3 : combo;
        ScoreText.text = score.ToString();

        // 벽돌 부서지는 애니메이션
        ColAni.SetTrigger("Break");
        S_Break.Play();
        StartCoroutine(ActiveFalse(Col));

        // 블럭 체크
        StopCoroutine("BlockCheck");
        StartCoroutine("BlockCheck",combo);
    }

    // %의 확률로 아이템이 나옴(아이템이 안 나올 수도 있다)
    void ItemGenerator(Vector2 ColTr)
    {
        int rand = Random.Range(0, 10000);
        if (rand < 1200)
        {
            string currentName = "";

            // 수정 코드
            switch (rand % 4)
            {
                case 0: currentName = "Item_TripleBall"; break; // 공이 3개 됨
                case 1: currentName = "Item_Big"; break;
                case 2: currentName = "Item_Small"; break;
                case 3: currentName = "Item_Gun"; break;
            }
            P_ItemSr.sprite = B[rand % 4 + 11];
            GameObject Item = Instantiate(P_Item, ColTr, Quaternion.identity);
            Item.name = currentName;
            Item.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 0.008f);
            Item.transform.SetParent(ItemsTr);
            Destroy(Item, 4);
        }
    }
    

    IEnumerator ActiveFalse(GameObject Col)
    {
        yield return new WaitForSeconds(0.2f);
        Col.SetActive(false);
    }

    GameObject BallCheck() // 죽는거 3개니까 계산 잘 해야돼... 
    {
        int ballCount = 0;
        GameObject ReturnBall = null;
        foreach (GameObject OneBall in GameObject.FindGameObjectsWithTag("Ball"))
        {
            ballCount++;
            ReturnBall = OneBall;
        }

        // 볼이 하나도 없을 때 라이프 깎임
        if (ballCount == 0)
        {
            if (Life2.activeSelf)
            {
                Life2.SetActive(false);
                StartCoroutine("BallReset");  // 목숨 하나 날린거니까 전체리셋하면 안된다
                S_Fail.Play();  // 죽었을 때 효과음
            }
            else if (Life1.activeSelf)
            {
                Life1.SetActive(false);
                StartCoroutine("BallReset");
                S_Fail.Play();
            }
            else
            {
                Life0.SetActive(false);
                GameOverPanel.SetActive(true);
                S_Fail.Play();
                Clear();
            }
        }
        return ReturnBall;
    }

    // ball에 힘을 줌 
    public void BallAddForce(Rigidbody2D ThisBallRg)
    {
        Vector2 dir = ThisBallRg.velocity.normalized;
        ThisBallRg.velocity = Vector2.zero;
        ThisBallRg.AddForce(dir * ballSpeed);
    }

    // 블럭 체크
    IEnumerator BlockCheck(int combo)
    {
        yield return new WaitForSeconds(0.5f);
        int blockCount = 0;

        if(comboBlockIndex != -1 && BlocksTr.childCount > comboBlockIndex && BlocksTr.GetChild(comboBlockIndex).gameObject.activeSelf)
        {
            blockCount++;

            if (combo > 1)
            {
                // Activate only the Combocol corresponding to the block where the combo occurred
                Combocol[comboBlockIndex].gameObject.SetActive(true);
                S_Combo.Play();
                StartCoroutine(DisableComboPanel(Combocol[comboBlockIndex].gameObject));
            }
        }

        // Check if all blocks are destroyed
        bool allBlocksDestroyed = true;
        for (int i = 0; i < BlocksTr.childCount; i++)
        {
            if (BlocksTr.GetChild(i).gameObject.activeSelf)
            {
                allBlocksDestroyed = false;
                break;
            }
        }

        // 승리
        if (allBlocksDestroyed && blockCount == 0)
        {
            WinPanel.SetActive(true);
            S_Victory.Play();
            Clear();
        }
        else { 
        // 가끔 아이템 흘림
            ItemGenerator(new Vector2(Random.Range(-9f, 9f), 4f));
            if (combo > 1)
            {
                Combocol[comboBlockIndex].gameObject.SetActive(true);
                S_Combo.Play();
                StartCoroutine(DisableComboPanel(Combocol[comboBlockIndex].gameObject));
            }
        }
    }

    IEnumerator DisableComboPanel(GameObject comboPanel)
    {
        yield return new WaitForSeconds(0.5f);
        comboPanel.SetActive(false);
    }

    // 승리 또는 게임오버시 호출됨 
    void Clear()
    {
        StopAllCoroutines();
        StartCoroutine("Item_BigOrSmall", true);
       // StartCoroutine("Item_SlowBall", true);
       // StartCoroutine("Item_FireBall", true);
       // StartCoroutine("Item_Magnet", true);
        StartCoroutine("Item_Gun", true);

        // 점수 초기화
       /* StageText.text = stage.ToString();
        score = 0;
        ScoreText.text = "0";
       */

        for (int i = 0; i < 3; i++) Ball[i].SetActive(false);
        PaddleSr.enabled = false;
    }

    
}
