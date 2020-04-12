using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField]
    //　ポーズした時・クリア時に表示するUIのプレハブ
    private GameObject pauseUIPrefab = null;
    [SerializeField]
    private GameObject clearUIPrefab = null;

    //　ポーズUI・クリアUIのインスタンス
    private GameObject pauseUIInstance;
    private GameObject clearUIInstance;

    GameManagement gameManegement;
    private bool clearFlag;
        
    // Start is called before the first frame update
    void Start()
    {
        this.gameManegement= GameObject.FindWithTag("GameManager").GetComponent<GameManagement>();
        this.clearFlag = this.gameManegement.ClearFlag;
    }


    // Update is called once per frame
    void Update()
    {
        if (this.clearFlag == true)
        {
            return;
        }
        else
        {
            this.clearFlag = this.gameManegement.ClearFlag;
            Clear_();
        }
        Pause_();
    }

    void Pause_()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (pauseUIInstance == null)
            {
                this.pauseUIInstance = GameObject.Instantiate(this.pauseUIPrefab) as GameObject;
                Time.timeScale = 0f;
            }
            else
            {
                Destroy(this.pauseUIInstance);
                Time.timeScale = 1f;
            }
        }
    }

    void Clear_()
    {
        if (this.clearFlag == true)
        {
            if (this.clearUIInstance == null)
            {
                this.clearUIInstance = GameObject.Instantiate(this.clearUIPrefab) as GameObject;
                return;
            }
        }
    }
}
