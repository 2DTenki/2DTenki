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

    private GameObject player;
    private bool clearFlag
        ;
    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.FindWithTag("Player");
        this.clearFlag = this.player.GetComponent<PlayerController>().player.ClearFlag;
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
            this.clearFlag = this.player.GetComponent<PlayerController>().player.ClearFlag;
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
                this.pauseUIInstance = GameObject.Instantiate(pauseUIPrefab) as GameObject;
                Time.timeScale = 0f;
            }
            else
            {
                Destroy(pauseUIInstance);
                Time.timeScale = 1f;
            }
        }
    }

    void Clear_()
    {
        if (this.clearFlag == true)
        {
            if (clearUIInstance == null)
            {
                this.clearUIInstance = GameObject.Instantiate(clearUIPrefab) as GameObject;
                return;
            }
        }
    }
}
