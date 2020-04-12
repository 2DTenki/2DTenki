using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSceneManager : MonoBehaviour
{
    public GameObject clear;
    public GameObject miss;

    //プレイヤのフラグだとゲームシーン以外でエラーが出るため
    private GameObject gm;
    private GameManagement gmFlag;

    // Start is called before the first frame update
    void Start()
    {
        this.gm = GameObject.FindWithTag("GameManager");
        this.gmFlag = gm.GetComponent<GameManagement>();

        if (this.gmFlag.ClearFlag == true)
        {
            Instantiate(clear);
        }
        else if (this.gmFlag.GameOverFlag == true)
        {
            Instantiate(miss);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
