using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverChip : MonoBehaviour
{
    Animator ani;
    public float RainTime = 0;
    private GameObject director;
    public ChangeWeather.WeatherState ws;

    bool iceFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        director = GameObject.FindWithTag("FieldDirector");
        ws = director.gameObject.GetComponent<ChangeWeather>().WS;
    }

    // Update is called once per frame
    void Update()
    {
        //ChangeWeather.WeatherState WS_pre = this.ws;

        ws = director.gameObject.GetComponent<ChangeWeather>().WS;

        if (ws == ChangeWeather.WeatherState.Rainy && !iceFlag)
        {
            RainTime += Time.deltaTime;

            //雨を降らせている時間でアニメーションの遷移を管理する
            ani.SetFloat("RainCnt", RainTime);

            ani.SetBool("Rain_Hard", false);
        }
        if (ws == ChangeWeather.WeatherState.Rainy_Hard && !iceFlag)
        {
            RainTime += Time.deltaTime;

            ani.SetFloat("RainCnt", RainTime);
            //川を溢れさせる
            ani.SetBool("Rain_Hard", true);
        }
        if (ws == ChangeWeather.WeatherState.Snowy)
        {
            //雪なら凍らせる
            ani.SetBool("ice_Bool", true);
            iceFlag = true;
        }
        if (ws == ChangeWeather.WeatherState.Snowy_Hard)
        {
            //大雪なら強く凍らせる
            ani.SetBool("Strong_Bool", true);
            iceFlag = true;
        }
        if (ws == ChangeWeather.WeatherState.Sunny_Hard)
        {
            //晴れ強なら元に戻す
            ani.SetBool("ice_Bool", false);
            ani.SetBool("Strong_Bool", false);
            iceFlag = false;
        }
    }
}
