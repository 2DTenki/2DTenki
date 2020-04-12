using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlowerScript : MonoBehaviour
{
    private Animator ani;
    private GameObject director;
    public ChangeWeather.WeatherState ws;

    GameObject Bud_col;
    GameObject flour_col;
    GameObject ice_col;

    bool RainyFlag = false;
    bool flourFlag = false;
    bool iceFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        director = GameObject.FindWithTag("FieldDirector");
        ws = director.gameObject.GetComponent<ChangeWeather>().WS;

        //　子オブジェクトを探して入れる。
        Bud_col = this.transform.Find("Bud_col").gameObject;
        flour_col = this.transform.Find("flour_col").gameObject;
        ice_col = this.transform.Find("ice_col").gameObject;
        //　Bud_col・flour_col・ice_col には芽の当たり判定。花の当たり判定。凍っているときの当たり判定。が入っている
        //　オブジェクトをアクティブにすれば　当たり判定が付き。　非アクティブにすれば当たり判定が消える
        flour_col.SetActive(false);
        ice_col.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        ChangeWeather.WeatherState WS_pre = this.ws;

        ws = director.gameObject.GetComponent<ChangeWeather>().WS;
        if (WS_pre != this.ws)
        {
            //雨が降ったフラグを建てる
            if (ws == ChangeWeather.WeatherState.Rainy)
            {
                RainyFlag = true;
            }
            //芽が凍っていなく、雨のフラグを建ててあって。今晴れ弱　なら花を咲かせるフラグを建てる
            if (!iceFlag && RainyFlag && ws == ChangeWeather.WeatherState.Sunny)
            {
                flourFlag = true;
                //flour_colに入っている子オブジェクトをアクティブにする
                flour_col.SetActive(true);

            }
            //凍っていなく。花のフラグが建っていて。今のフラグが大雨なら。すべての子オブジェクトを非アクティブにする
            if (!iceFlag && RainyFlag && ws == ChangeWeather.WeatherState.Rainy_Hard)
            {
                Bud_col.SetActive(false);
                flour_col.SetActive(false);
                ice_col.SetActive(false);
            }

            if (RainyFlag)//1つ前の天気が雨だったら
            {
                switch (ws)
                {
                    case ChangeWeather.WeatherState.Cloudy:
                        RainyFlag = false;　//曇りだったら。雨のフラグをOFFにする
                        break;

                    case ChangeWeather.WeatherState.Rainy_Hard:
                        //大雨なら、芽と花の枯れるアニメーターのフラグをtrueにする
                        ani.SetBool("Bud_Wither_Bool", true);
                        ani.SetBool("Flowering_Wither_Bool", true);

                        RainyFlag = false;//雨のフラグをOFFにする
                        break;

                    case ChangeWeather.WeatherState.Snowy:
                        RainyFlag = false;///雪だったら、雨のフラグをOOFにする
                        break;
                    case ChangeWeather.WeatherState.Sunny:
                        //晴れだったら花を咲かせるアニメーションをtrue
                        ani.SetBool("Flowering_Bool", true);

                        RainyFlag = false;
                        break;

                }
            }
            //凍っていて。晴れ強だったら
            else if (iceFlag && ws == ChangeWeather.WeatherState.Sunny_Hard)
            {
                //大雪→雨→晴れ　の流れで晴れ強にすると　
                //凍るアニメーションが終わったら花を咲かせてしまうので、アニメーターのフラグをオフにする
                ani.SetBool("Flowering_Bool", false);

                //　晴れ強にすると　枯れるアニメーションがオンになるため、
                //溶かそうと晴れ強にしても枯れて詰んでしまうので　
                //凍っている時に晴れ強にすると枯れるアニメーションをオフにする
                ani.SetBool("Bud_Wither_Bool", false);
                ani.SetBool("Flowering_Wither_Bool", false);

                //溶けるアニメーターのフラグをオンにする
                ani.SetBool("Bud_Melt_ Bool", true);
                ani.SetBool("Flower_Melt_Bool", true);

                //凍るアニメーターのフラグをオフ
                ani.SetBool("Bud_Freeze_Bool", false);
                ani.SetBool("Flower_Freeze_Bool", false);
                iceFlag = false;

                ice_col.SetActive(false);//凍る時の当たり判定をアクティブにする
            }
            //花が咲いていたら
            else if (!RainyFlag && flourFlag)
            {
                switch (ws)
                {
                    case ChangeWeather.WeatherState.Snowy_Hard:
                        //大雪なら凍らせるアニメーションをオンにする
                        ani.SetBool("Flower_Freeze_Bool", true);
                        //溶けるアニメーションをオフにする
                        ani.SetBool("Flower_Melt_Bool", false);

                        iceFlag = true;//凍りのフラグをオフにする
                        ice_col.SetActive(true);　//凍りの当たり判定をアクティブにする

                        break;

                    case ChangeWeather.WeatherState.Sunny_Hard:

                        //花を枯らすアニメーションをオンにする
                        ani.SetBool("Flowering_Wither_Bool", true);

                        Bud_col.SetActive(false);
                        flour_col.SetActive(false);
                        ice_col.SetActive(false);
                        //全ての当たり判定を非アクティブにする
                        break;
                }
            }
            //　それ以外の場合は
            else
            {
                switch (ws)
                {

                    case ChangeWeather.WeatherState.Snowy_Hard://大雪

                        //凍るアニメーションのフラグを、芽と花を両方trueにする
                        ani.SetBool("Bud_Freeze_Bool", true);
                        //溶けるアニメーターのフラグを芽と花　両方オフ
                        ani.SetBool("Bud_Melt_ Bool", false);

                        iceFlag = true;//凍っているフラグをオンにする
                        ice_col.SetActive(true);//ice_colをアクティブにして、凍っているときの当たり判定を出現させる

                        break;

                    case ChangeWeather.WeatherState.Sunny_Hard://晴れ強
                        //枯らすアニメーションをオンにする
                        ani.SetBool("Bud_Wither_Bool", true);
                        ani.SetBool("Flowering_Wither_Bool", true);

                        Bud_col.SetActive(false);
                        flour_col.SetActive(false);
                        ice_col.SetActive(true);
                        //全ての当たり判定を非アクティブにする
                        break;
                }
            }

        }
    }
}
