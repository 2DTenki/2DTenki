using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    Animator ani;
    Collider2D Col;
    private GameObject director;
    public ChangeWeather.WeatherState ws;
    public float rainCnt;

    private float bridgePosY;
    bool iceFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        Col = GetComponent<Collider2D>();
        director = GameObject.FindWithTag("FieldDirector");
        ws = director.gameObject.GetComponent<ChangeWeather>().WS;

        this.bridgePosY = -3.2f;
        this.transform.localPosition = new Vector2(3.5f, this.bridgePosY);
    }

    // Update is called once per frame
    void Update()
    {
        ChangeWeather.WeatherState WS_pre = this.ws;

        ws = director.gameObject.GetComponent<ChangeWeather>().WS;

        if (WS_pre != this.ws)
        {
            switch (ws)
            {
                case ChangeWeather.WeatherState.Rainy_Hard:
                    //大雨にしたら　橋が壊れるアニメーションを再生
                    ani.SetBool("Break_Bool", true);
                    Col.enabled = false;
                    break;

                case ChangeWeather.WeatherState.Snowy:
                    iceFlag = true;
                    break;

                case ChangeWeather.WeatherState.Snowy_Hard:
                    //大雪で凍らせるアニメーションを再生
                    ani.SetBool("ice_Bool", true);
                    //溶かすアニメータ―をオフ
                    ani.SetBool("Melt_Bool", false);
                    iceFlag = true;

                    break;
                case ChangeWeather.WeatherState.Sunny_Hard:
                    //晴れ強で溶けるアニメーションを再生
                    ani.SetBool("Melt_Bool", true);
                    //凍らせるアニメーターをオフ
                    ani.SetBool("ice_Bool", false);
                    iceFlag = false;
                    break;
            }
        }
        if (ws == ChangeWeather.WeatherState.Rainy && !iceFlag)
        {
            rainCnt += Time.deltaTime;
            if (this.bridgePosY <= -2.7f)
            {
                this.bridgePosY += Time.deltaTime / 5.25f;
            }
            else if (this.bridgePosY <= -2.4f)
            {
                this.bridgePosY += Time.deltaTime / 3.0f;
            }
            else if (this.bridgePosY <= -1.1f)
            {
                this.bridgePosY += Time.deltaTime /3.5f;
            }
            else if (this.bridgePosY < -0.4f)
            {
                this.bridgePosY += Time.deltaTime /3.0f;
            }
            else if (this.bridgePosY >= -0.4f)
            {
                this.bridgePosY = -0.4f;
            }
            //雨を降らせている時間で橋の位置を変える
            this.transform.localPosition = new Vector2(3.5f, this.bridgePosY);


            //if (rainCnt >= 9.1f)-3.2
            //{
            //    this.transform.localPosition = new Vector2(3.5f, -2.4f);
            //}
            //if (rainCnt >= 17.1f)
            //{
            //    this.transform.localPosition = new Vector2(3.5f, -1.7f);
            //}
            //if (rainCnt >= 25.1f)
            //{
            //    this.transform.localPosition = new Vector2(3.5f, -1.09f);
            //}
            //if (rainCnt >= 32.1f)
            //{
            //    this.transform.localPosition = new Vector2(3.5f, -0.4f);
            //}
        }
    }
}
