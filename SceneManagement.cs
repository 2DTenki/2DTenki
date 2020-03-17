using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //タイトルシーン時
        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            TitleScene();
        }
        //ゲームシーン時
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            GameScene();
        }
        //ロードシーン時
        if (SceneManager.GetActiveScene().name == "StageLoadScene")
        {
            LoadScene();
        }
        //エンディングシーン時
        if (SceneManager.GetActiveScene().name == "EndingScene")
        {
            EndingScene();
        }
    }


    void TitleScene()
    {
        //ゲームシーンへ
        if (Input.GetKey(KeyCode.S))
        {
            //StartCoroutine(WaitForScene());
            SceneManager.LoadScene("GameScene");
        }
        //ゲーム終了
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void GameScene()
    {
        //タイトルシーンへ
        if (Input.GetKey(KeyCode.Escape))
        {
            GetComponent<GameManagement>().nowStageNum = 1;
            //StartCoroutine(WaitForScene("TitleScene"));
            GameObject obj = GameObject.FindWithTag("Goal");
            Goal goal = obj.GetComponent<Goal>();
            SceneLoad("TitleScene");
        }
    }

    void LoadScene()
    {
        //ゲームシーンへ
        if (Input.GetKey(KeyCode.S))
        {
            //StartCoroutine(WaitForScene("GameScene"));
            SceneLoad("GameScene");
        }
        //タイトルシーンへ
        if (Input.GetKey(KeyCode.Escape))
        {
            GetComponent<GameManagement>().nowStageNum = 1;
            //StartCoroutine(WaitForScene("TitleScene"));
            SceneLoad("TitleScene");
        }
    }

    void EndingScene()
    {
        //タイトルシーンへ
        if (Input.GetKey(KeyCode.T))
        {
            GetComponent<GameManagement>().nowStageNum = 1;
            //StartCoroutine(WaitForScene("TitleScene"));
            SceneLoad("TitleScene");
        }
    }

    //----------------------------------------------------------------------
    //シーン呼び出し

    IEnumerator WaitForScene(string sceneName)
    {
        //シーンを非同期で読込し、読み込まれるまで待機する
        yield return SceneManager.LoadSceneAsync(sceneName);
    }

    void SceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
