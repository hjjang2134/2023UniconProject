using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue info;

    public void Trigger()
    {
        // DialogueSystem 찾아오기
        var system = FindObjectOfType<DialogueSystem>();
        if (system != null)
        {
            system.Begin(info);
        }

        // DialogueSystemVer2 찾아오기
        var systemVer2 = FindObjectOfType<DialogueSystemVer2>();
        if (systemVer2 != null)
        {
            systemVer2.Begin(info);
        }

        // 만약 둘 다 찾을 수 없다면 에러 출력
        if (system == null && systemVer2 == null)
        {
            Debug.LogError("Dialogue systems not found!");
        }
    }
}
