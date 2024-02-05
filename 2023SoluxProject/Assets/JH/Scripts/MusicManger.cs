using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public GameObject Music;

    private void Start()
    {
        DontDestroyOnLoad(Music);

        // SceneManager의 sceneLoaded 이벤트에 OnSceneLoaded 함수를 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene loadedScene, LoadSceneMode mode)
    {
        // 2_Opening 씬일 때 Music 오브젝트를 파괴
        if (loadedScene.name == "2_Opening")
        {
            Destroy(Music);

            // SceneManager의 sceneLoaded 이벤트에서 OnSceneLoaded 함수를 제거
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
