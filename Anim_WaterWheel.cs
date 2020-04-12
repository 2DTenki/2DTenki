using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_WaterWheel : MonoBehaviour
{
    public bool Move, Freeze;
    public Animator animator;
    private GameObject director;
    public ChangeWeather.WeatherState ws;

    // Start is called before the first frame update
    void Start()
    {
        this.director = GameObject.FindWithTag("FieldDirector");
        this.ws = this.director.gameObject.GetComponent<ChangeWeather>().WS;
        this.Move = false;
        this.Freeze = false;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeWeather.WeatherState WS_pre = this.ws;

        this.ws = this.director.gameObject.GetComponent<ChangeWeather>().WS;
        if (WS_pre != this.ws)
        {
            switch (this.ws)
            {

                //水車回転
                case ChangeWeather.WeatherState.Rainy_Hard:
                    this.Move = true;
                    Invoke("Move_WaterWheel", 1.5f);
                    break;

                //水車凍結
                case ChangeWeather.WeatherState.Snowy_Hard:
                    this.Freeze = true;
                    Invoke("Freeze_WaterWheel", 0.5f);
                    break;

                //水車解凍
                case ChangeWeather.WeatherState.Sunny_Hard:
                    this.Freeze = false;
                    Invoke("Freeze_WaterWheel", 0.5f);
                    break;

                default:
                    this.Move = false;
                    Invoke("Move_WaterWheel", 0.5f);
                    break;

            }
        }
    }

    void Move_WaterWheel()
    {
        this.animator.SetBool("Move", this.Move);
    }

    void Freeze_WaterWheel()
    {
        this.animator.SetBool("Freeze", this.Freeze);
    }
}