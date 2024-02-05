using UnityEngine;
using TMPro;

public class InputHandler : MonoBehaviour
{
    public TMP_InputField inputField;

    public void SaveUsername()
    {
        DataHolder.username = inputField.text;
    }
}
