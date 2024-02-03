using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI txtName;
    public  TextMeshProUGUI txtSentence;

    Queue<string> sentences = new Queue<string>();

    public void Begin(Dialogue info)
    {

        sentences.Clear();

        txtName.text = info.name;

        foreach(var sentence in info.sentences)
        {
            sentences.Enqueue(sentence);
        }

        Next();
    }

    public void Next(){
        if(sentences.Count == 0)
        {
            End();
            return;
        }

        txtSentence.text = sentences.Dequeue();
    }

    private void End()
    {
        txtSentence.text = string.Empty;
        SceneManager.LoadScene("3_map");
    }
    
}

