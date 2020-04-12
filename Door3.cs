using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door3 : MonoBehaviour
{
    //アニメーション用
    public Animator animator;
    private bool Freeze = false;
    public bool DoorFullOpenFlag = false;
    public bool DoorHalfOpenFlag = false;

    public float timeCnt;
    public int hikariPower;         //1なら晴れ弱、2なら晴れ強
    private ChangeWeather.WeatherState ws;
    private GameObject director;
    //フィールドの鏡の数
    private GameObject[] mirrors;
    private int mirrorCnt = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.director = GameObject.FindWithTag("FieldDirector");
        this.ws = this.director.gameObject.GetComponent<ChangeWeather>().WS;

        Find_Mirrors();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeWeather.WeatherState preWS = this.ws;
        this.ws = this.director.gameObject.GetComponent<ChangeWeather>().WS;
        if (preWS != this.ws)
        {
            timeCnt = 0;
        }
        switch (mirrorCnt)
        {
            case 1:
                //鏡が1台あるとき(ステージ4の処理)
                Mirror_1();
                break;
            case 2:
                //鏡が2台あるとき(ステージ9の処理)
                Mirror_2();
                break;
        }

    }

    //フィールド上にある鏡の数を数える
    private void Find_Mirrors()
    {
        this.mirrors = GameObject.FindGameObjectsWithTag("mirror");
        foreach (GameObject obj in mirrors)
        {
            mirrorCnt++;
        }
    }

    private void Mirror_1()
    {
        switch (this.ws)
        {
            default:

                ResetCollider();
                this.DoorFullOpenFlag = false;
                this.DoorHalfOpenFlag = false;
                //扉のアニメーション
                animator.SetBool("FullOpen", this.DoorFullOpenFlag);//仮OK
                animator.SetBool("HalfOpen", this.DoorHalfOpenFlag);//仮OK
                break;

            //晴れ弱(半開き)
            case ChangeWeather.WeatherState.Sunny:
                timeCnt += Time.deltaTime;

                ResetCollider();
                //扉に半開きのアニメかける
                //曇り→晴れ（弱）
                if (this.timeCnt >= 3.5f &&
                    this.DoorFullOpenFlag == false && Freeze == false)
                {
                    DoorHalfOpenFlag = true;
                    animator.SetBool("HalfOpen", this.DoorHalfOpenFlag);
                }
                //晴れ（強）→晴れ（弱）
                if (DoorFullOpenFlag == true)
                {
                    DoorFullOpenFlag = false;
                    this.animator.SetBool("FullOpen", this.DoorFullOpenFlag);//仮OK

                    DoorHalfOpenFlag = true;
                    this.animator.SetBool("HalfOpen", this.DoorHalfOpenFlag); //全開→半開きアニメーション
                }
                break;

            //晴れ強(開く)
            case ChangeWeather.WeatherState.Sunny_Hard:
                this.timeCnt += Time.deltaTime;
                //通常時
                if (/*this.timeCnt >= 0.5f &&*/
                    this.DoorFullOpenFlag == false && this.DoorHalfOpenFlag == false)//ゲージみたいなやつほしい
                {
                    this.DoorFullOpenFlag = true;
                    //扉のアニメーション
                    this.animator.SetBool("FullOpen", this.DoorFullOpenFlag);//仮OK
                    Invoke("Move_Collider", 2.5f);
                }
                //晴れ弱通過後
                else if (this.timeCnt >= 1.0f &&
                         this.DoorFullOpenFlag == false && this.DoorHalfOpenFlag == true)
                {
                    //半開き→全開アニメーション
                    DoorHalfOpenFlag = false;
                    this.animator.SetBool("HalfOpen", this.DoorHalfOpenFlag);
                    this.DoorFullOpenFlag = true;
                    this.animator.SetBool("FullOpen", this.DoorFullOpenFlag);//
                    Invoke("Move_Collider", 2.5f);
                }
                //解凍してから
                else if (this.timeCnt >= 1.0f && this.Freeze == true)
                {
                    this.Freeze = false;
                    this.animator.SetBool("Freeze", this.Freeze);
                }
                break;

            //雪強(凍る)
            case ChangeWeather.WeatherState.Snowy_Hard:
                this.timeCnt += Time.deltaTime;
                if (this.timeCnt >= 0.5f)
                {
                    this.Freeze = true;
                    this.animator.SetBool("Freeze", this.Freeze);
                }
                break;
        }
    }

    private void Mirror_2()
    {
        switch (this.ws)
        {
            default:

                ResetCollider();
                this.DoorFullOpenFlag = false;
                //扉のアニメーション
                animator.SetBool("FullOpen", this.DoorFullOpenFlag);
                break;

            //晴れ弱(全開き)
            case ChangeWeather.WeatherState.Sunny:
                timeCnt += Time.deltaTime;

                //扉にアニメかける
                //曇り→晴れ（弱）
                if (this.timeCnt >= 3.5f &&
                    this.DoorFullOpenFlag == false && Freeze == false)
                {
                    DoorFullOpenFlag = true;
                    animator.SetBool("FullOpen", this.DoorFullOpenFlag);
                    Invoke("Move_Collider", 2.5f);
                }
                break;

            //晴れ強(開く)
            case ChangeWeather.WeatherState.Sunny_Hard:
                this.timeCnt += Time.deltaTime;

                //通常時:既に開いていた場合は何もしない
                if (/*this.timeCnt >= 0.5f &&*/
                    this.DoorFullOpenFlag == false && this.Freeze == false)//ゲージみたいなやつほしい
                {
                    this.DoorFullOpenFlag = true;
                    //扉のアニメーション
                    this.animator.SetBool("FullOpen", this.DoorFullOpenFlag);//仮OK
                    Invoke("Move_Collider", 2.5f);
                }

                //凍っていたときは解凍する
                else if (this.timeCnt >= 1.0f && this.Freeze == true)
                {//invoke
                    Invoke("Melt", 0.5f);
                    //this.Freeze入れるとInvoke()でタイミングずれているのでアニメできない
                    this.animator.SetBool("Freeze", false);
                }
                break;

            //雪強(凍る)
            case ChangeWeather.WeatherState.Snowy_Hard:
                this.timeCnt += Time.deltaTime;
                if (this.timeCnt >= 0.5f)
                {
                    this.Freeze = true;
                    this.animator.SetBool("Freeze", this.Freeze);
                }
                break;
        }

    }
    private void Move_Collider()
    {
        GetComponent<Collider2D>().offset = new Vector2(0.4f, 1.75f);
    }
    private void ResetCollider()
    {
        GetComponent<Collider2D>().offset = new Vector2(0.4f, 0.25f);
    }
    private void Melt()
    {
        this.Freeze = false;
    }
}