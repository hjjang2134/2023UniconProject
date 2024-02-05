using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public GameObject ending;
    public float scrollSpeed = 250f;
    private RectTransform creditsTransform;
    private bool isMousePressed = false;
    public float stopYPosition = 6355f;
    public Button btn;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(2340, 1080, true);
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
                // ���콺 ��ư�� �������� ������ �⺻ �ӵ��� ��ũ��
                if (isMousePressed)
                {
                    isMousePressed = false;
                    creditsTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
                }
            }
        }
        else
        {
            btn.interactable = true;
        }
    }

   
}
