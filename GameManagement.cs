using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
                if (instance == null)
                {
                    Debug.LogError(typeof(T) + "がシーンに存在しません。");
                }
            }
            return instance;
        }
    }
}
public class GameManagement : SingletonMonoBehaviour<GameManagement>
{
    //[System.NonSerialized]
    public int nowStageNum = 1;  //現在のステージ番号（1から）

    public int maxStageNum = 10; //総ステージ数

    // Start is called before the first frame update
    void Start()
    {
        Awake();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    //次のステージへ
    public void NextStage()
    {
        nowStageNum += 1;
        if (nowStageNum <= maxStageNum)
        {
            //コルーチンを実行
            StartCoroutine(WaitForLoadScene());
        }
        else
        {
            //コルーチンを実行
            StartCoroutine(WaitForEndingScene());
        }
    }

    IEnumerator WaitForLoadScene()
    {
        //シーンを非同期で読込し、読み込まれるまで待機する
        yield return SceneManager.LoadSceneAsync("StageLoadScene");
    }

    IEnumerator WaitForEndingScene()
    {
        //シーンを非同期で読込し、読み込まれるまで待機する
        yield return SceneManager.LoadSceneAsync("EndingScene");
    }

    public void GameOver()
    {

    }
}
