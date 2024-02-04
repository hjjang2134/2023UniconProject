using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Change : MonoBehaviour
{
    public void SceneChangeop(){
        SceneManager.LoadScene("2_Opening");
    }

     public void SceneChangenick(){
        SceneManager.LoadScene("2_1Nickname");
    }

    public void SceneChangemap1(){
        SceneManager.LoadScene("4_GameHJ");
    }

    public void SceneChangemap2(){
        SceneManager.LoadScene("5_GameNY");
    }

    public void SceneChangemap3(){
        SceneManager.LoadScene("6_GameYJ");
    }
}
