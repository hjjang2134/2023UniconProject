using UnityEngine;
using TMPro;

public class InputText : MonoBehaviour
{
    // 인스펙터에서 연결할 TextMeshPro 변수
    public TMP_InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs에서 저장된 입력을 불러와 InputField에 설정
        LoadSavedInput();
    }

    // Save 버튼 등에서 호출할 메서드: 현재 InputField에 입력된 텍스트를 PlayerPrefs에 저장
    public void SaveInput()
    {
        string userInput = inputField.text;
        PlayerPrefs.SetString("PlayerInput", userInput);
        PlayerPrefs.Save();
    }

    // 시작 시 호출되는 메서드: 저장된 입력을 불러와 InputField에 설정
    void LoadSavedInput()
    {
        if (PlayerPrefs.HasKey("PlayerInput"))
        {
            string savedInput = PlayerPrefs.GetString("PlayerInput");
            inputField.text = savedInput;
        }
    }
}
