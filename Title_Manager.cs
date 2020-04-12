using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Title_Manager : MonoBehaviour
{
    //タイトル最初のボタン3個
    public Button StartButton;
    public Button StageSelectButton;
    public Button ExitButton;
    //ステージセレクトのボタン11個
    public Button Stage1Button;
    public Button Stage2Button;
    public Button Stage3Button;
    public Button Stage4Button;
    public Button Stage5Button;
    public Button Stage6Button;
    public Button Stage7Button;
    public Button Stage8Button;
    public Button Stage9Button;
    public Button Stage10Button;
    public Button BackButton;

    // Start is called before the first frame update
    void Start()
    {
        StageSelectDisable();
        StartButton.Select();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick(int number)
    {
        //押されたボタンそれぞれの役割
        switch (number)
        {
            case 0:
                SceneManager.LoadScene("GameScene");
                break;
            case 1:
                TitleButtonDisable();
                StageSelectEnable();
                Stage1Button.Select();
                break;
            case 2:
                Application.Quit();
                break;
            case 3:
                StageSelectDisable();
                TitleButtonEnable();
                StartButton.Select();
                break;
            default:
                break;
        }
    }
    //初期タイトル画面での3個のボタンを不可視化、使用不可にする 
    public void TitleButtonDisable()
    {
        StartButton.gameObject.SetActive(false);
        StageSelectButton.gameObject.SetActive(false);
        ExitButton.gameObject.SetActive(false);
    }
    //初期タイトル画面での3個のボタンを可視化、使用可にする
    public void TitleButtonEnable()
    {
        StartButton.gameObject.SetActive(true);
        StageSelectButton.gameObject.SetActive(true);
        ExitButton.gameObject.SetActive(true);
    }
    //ステージセレクト画面での11個のボタンを不可視化、使用不可にする
    public void StageSelectDisable()
    {
        Stage1Button.gameObject.SetActive(false);
        Stage2Button.gameObject.SetActive(false);
        Stage3Button.gameObject.SetActive(false);
        Stage4Button.gameObject.SetActive(false);
        Stage5Button.gameObject.SetActive(false);
        Stage6Button.gameObject.SetActive(false);
        Stage7Button.gameObject.SetActive(false);
        Stage8Button.gameObject.SetActive(false);
        Stage9Button.gameObject.SetActive(false);
        Stage10Button.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(false);
    }
    //初期タイトル画面での11個のボタンを可視化、使用可にする
    public void StageSelectEnable()
    {
        Stage1Button.gameObject.SetActive(true);
        Stage2Button.gameObject.SetActive(true);
        Stage3Button.gameObject.SetActive(true);
        Stage4Button.gameObject.SetActive(true);
        Stage5Button.gameObject.SetActive(true);
        Stage6Button.gameObject.SetActive(true);
        Stage7Button.gameObject.SetActive(true);
        Stage8Button.gameObject.SetActive(true);
        Stage9Button.gameObject.SetActive(true);
        Stage10Button.gameObject.SetActive(true);
        BackButton.gameObject.SetActive(true);
    }


    //ステージセレクトボタンから任意のステージへ(ステージ1～10)
    public void OnButton_1()
    {
        //シーン上の"GameManager"をTagから探し、それを取得
        GameManagement gameManegement = GameObject.FindWithTag("GameManager").GetComponent<GameManagement>();

        //取得したデータから現在のステージ番号を書き換え
        gameManegement.nowStageNum = 1;

        //ゲームシーンへ飛ばし、現在のステージ番号に応じたステージ（マップ）を構築する
        //（構築は"CreateMap_FromTxt"にて）
        SceneManager.LoadScene("GameScene");
    }
    public void OnButton_2()
    {
        GameManagement gameManegement = GameObject.FindWithTag("GameManager").GetComponent<GameManagement>();
        gameManegement.nowStageNum = 2;
        SceneManager.LoadScene("GameScene");
    }
    public void OnButton_3()
    {
        GameManagement gameManegement = GameObject.FindWithTag("GameManager").GetComponent<GameManagement>();
        gameManegement.nowStageNum = 3;
        SceneManager.LoadScene("GameScene");
    }
    public void OnButton_4()
    {
        GameManagement gameManegement = GameObject.FindWithTag("GameManager").GetComponent<GameManagement>();
        gameManegement.nowStageNum = 4;
        SceneManager.LoadScene("GameScene");
    }
    public void OnButton_5()
    {
        GameManagement gameManegement = GameObject.FindWithTag("GameManager").GetComponent<GameManagement>();
        gameManegement.nowStageNum = 5;
        SceneManager.LoadScene("GameScene");
    }
    public void OnButton_6()
    {
        GameManagement gameManegement = GameObject.FindWithTag("GameManager").GetComponent<GameManagement>();
        gameManegement.nowStageNum = 6;
        SceneManager.LoadScene("GameScene");
    }
    public void OnButton_7()
    {
        GameManagement gameManegement = GameObject.FindWithTag("GameManager").GetComponent<GameManagement>();
        gameManegement.nowStageNum = 7;
        SceneManager.LoadScene("GameScene");
    }
    public void OnButton_8()
    {
        GameManagement gameManegement = GameObject.FindWithTag("GameManager").GetComponent<GameManagement>();
        gameManegement.nowStageNum = 8;
        SceneManager.LoadScene("GameScene");
    }
    public void OnButton_9()
    {
        GameManagement gameManegement = GameObject.FindWithTag("GameManager").GetComponent<GameManagement>();
        gameManegement.nowStageNum = 9;
        SceneManager.LoadScene("GameScene");
    }
    public void OnButton_10()
    {
        GameManagement gameManegement = GameObject.FindWithTag("GameManager").GetComponent<GameManagement>();
        gameManegement.nowStageNum = 10;
        SceneManager.LoadScene("GameScene");
    }
}
