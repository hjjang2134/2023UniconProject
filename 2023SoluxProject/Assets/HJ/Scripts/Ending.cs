using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public GameObject ending;
    public float scrollSpeed = 250f;
    private RectTransform creditsTransform;
    private bool isMousePressed = false;
    public float stopYPosition = 6355f;
    public GameObject Go_Main;
    // Start is called before the first frame update
    void Start()
    {
        creditsTransform = ending.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (creditsTransform.anchoredPosition.y <= stopYPosition)
        {
            creditsTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

            if (Input.GetMouseButton(0))
            {
                isMousePressed = true;
                creditsTransform.anchoredPosition += Vector2.up * 600f * Time.deltaTime;
            }
            else
            {
                // 마우스 버튼이 눌려있지 않으면 기본 속도로 스크롤
                if (isMousePressed)
                {
                    isMousePressed = false;
                    creditsTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
                }
            }
        }
        else
        {
            Go_Main.SetActive(true);
        }
    }

   
}
