using UnityEngine;
using TMPro;

public class UsernameUpdater : MonoBehaviour
{
    public TextMeshProUGUI usernameText;

    void Start()
    {
        UpdateUsernameText();
    }

    void UpdateUsernameText()
    {
        usernameText.text = DataHolder.username;
    }
}
