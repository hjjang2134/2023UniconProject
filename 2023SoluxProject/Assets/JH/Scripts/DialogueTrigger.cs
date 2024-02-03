using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue info;

    public void Trigger(){
        var system = FindObjectOfType<DialogueSystem>();
        system.Begin(info);
    }
}
