/*using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public sealed class Score : MonoBehaviour
{
    public static Score Instance { get; private set; }

    private int _score;

    public int Score_
    {
        get => _score;

        set
        {
            if (_score == value) return;

            _score = value;

            scoreText.SetText($"{_score} / 50");
                
        }
    }

    [SerializeField] private TextMeshProUGUI scoreText;

    private void Awake() => Instance = this;
}*/

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class Score : MonoBehaviour
{
    public static Score Instance { get; private set; }

    private Dictionary<Item, int> colorScores = new Dictionary<Item, int>(); // 각 색깔의 점수를 저장할 딕셔너리

    public Button btn;

    public int RedScore => GetColorScore(ItemDatabase.Items[3]); // 빨간색 점수
    public int YellowScore => GetColorScore(ItemDatabase.Items[4]); // 노란색 점수
    public int GreenScore => GetColorScore(ItemDatabase.Items[1]); // 초록색 점수
    public int BlueScore => GetColorScore(ItemDatabase.Items[0]); // 파란색 점수
    public int PurpleScore => GetColorScore(ItemDatabase.Items[2]); // 보라색 점수

    [SerializeField] public TextMeshProUGUI redScoreText;
    [SerializeField] public TextMeshProUGUI yellowScoreText; 
    [SerializeField] public TextMeshProUGUI greenScoreText; 
    [SerializeField] public TextMeshProUGUI blueScoreText; 
    [SerializeField] public TextMeshProUGUI purpleScoreText;

    private void Awake() {
        Instance = this;
    } 

    private void Start()
    {
        // 초기화: 각 색깔의 점수를 0으로 설정
        ResetScore();
        //버튼안보이게 
        btn.gameObject.SetActive(false);
    }
    public void ResetScore()
    {
        foreach (var item in ItemDatabase.Items)
        {
            colorScores[item] = 0;
        }
        UpdateUI();
    }

    public void Update()
    {
        isClear();

    }

    public void AddScore(Item color, int score)
    {
        // 색깔 별 점수 추가
        colorScores[color] += score;

        // UI 갱신
        UpdateUI();
    }

    

    private int GetColorScore(Item color)
    {
        // 색깔 별 점수 반환
        return colorScores[color];
    }

    private void UpdateUI()
    {
        // UI 갱신: 각 색깔의 점수를 표시
        redScoreText.SetText($"{RedScore} / 10");
        yellowScoreText.SetText($"{YellowScore} / 10");
        greenScoreText.SetText($"{GreenScore} / 10");
        blueScoreText.SetText($"{BlueScore} / 10");
        purpleScoreText.SetText($"{PurpleScore} / 10");
    }

    private void isClear()
    {
        if (RedScore >= 10 && YellowScore >= 10 && GreenScore >= 10 && BlueScore >= 10 && PurpleScore >= 10)
        {
            //성공!! 다음으로 넘어가기 버튼
            btn.gameObject.SetActive(true);
        }
    }



}
