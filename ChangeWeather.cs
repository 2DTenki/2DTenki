﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeather : MonoBehaviour
{
    //天候
    public enum WeatherState
    {
        Cloudy,         //曇り（ベース）
        Rainy,          //雨
        Rainy_Hard,     //雨（強）
        Sunny,          //晴れ
        Sunny_Hard,     //晴れ（強）
        Snowy,          //雪
        Snowy_Hard,     //雪(強)
    }
    public WeatherState WS;

    private GameManagement gameManegement;
    private GameObject player;
    private bool clearFlag;
    private bool GameOverFlag;

    //天候変化回数設定用
    private PlayerController playerController;

    public bool Start_CoolTime;
    public float coolTime;
    private float nowTime;

    // Start is called before the first frame update
    void Start()
    {
        //初期天候を曇りに
        this.WS = WeatherState.Cloudy;

        this.player = GameObject.FindWithTag("Player");
        this.playerController = player.GetComponent<PlayerController>();
        this.gameManegement= GameObject.FindWithTag("GameManager").GetComponent<GameManagement>();

        this.Start_CoolTime = false;
        this.nowTime = 0;
    }



    // Update is called once per frame
    void Update()
    {
        //未クリア・ゲームオーバー時のみ変更可能
        if (this.gameManegement.ClearFlag == true ||
            this.gameManegement.GameOverFlag == true)
        {
            return;
        }

        WeatherState pre = this.WS;

        CoolTime();

        //非クールタイム時のみ変更可能
        if (Start_CoolTime == false)
        {
            if (Input.GetButton("Fire1"))
            {
                //天候変化（ベースの天気）
                Weather_ChangeBase();
            }

            //天候変化（強さ）
            Weather_ChangeStrength();

            if (pre != this.WS)
            {
                //変更可能な回数を1回減らす
                this.playerController.player.changeCnt -= 1;
                if (this.playerController.player.changeCnt == 0)
                {
                    this.gameManegement.GameOverFlag = true;
                }
                this.Start_CoolTime = true;
                //天候表示
                Debug.Log(this.WS);
                Debug.Log(this.playerController.player.changeCnt);
            }
        }
    }

    private void CoolTime()
    {
        if (this.Start_CoolTime /*== true*/)
        {
            this.nowTime += Time.deltaTime;

            if (this.nowTime >= this.coolTime)
            {
                nowTime = 0;
                Start_CoolTime = false;
            }
        }
    }

    //天気の変更
    void Weather_ChangeBase()
    {

        float weatherX = Input.GetAxis("Horizontal");
        float weatherY = Input.GetAxis("Vertical");

        if (weatherX > 0.1f)
        {
            this.WS = WeatherState.Rainy;
        }

        if (weatherX < -0.1f)
        {
            this.WS = WeatherState.Sunny;
        }

        if (weatherY > 0.1f)
        {
            this.WS = WeatherState.Snowy;
        }

        if (weatherY < -0.1f)
        {
            this.WS = WeatherState.Cloudy;
        }
    }

    //天気の強弱の変更
    void Weather_ChangeStrength()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            switch (this.WS)
            {
                case WeatherState.Sunny:
                    this.WS = WeatherState.Sunny_Hard;
                    break;

                case WeatherState.Sunny_Hard:
                    this.WS = WeatherState.Sunny;
                    break;

                case WeatherState.Rainy:
                    this.WS = WeatherState.Rainy_Hard;
                    break;

                case WeatherState.Rainy_Hard:
                    this.WS = WeatherState.Rainy;
                    break;

                case WeatherState.Snowy:
                    this.WS = WeatherState.Snowy_Hard;
                    break;

                case WeatherState.Snowy_Hard:
                    this.WS = WeatherState.Snowy;
                    break;
            }
        }
    }
}