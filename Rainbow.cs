using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rainbow : MonoBehaviour
{

    public ChangeWeather.WeatherState ws;
    private GameObject director;

    public Animator animator;
    private bool Appear = false;

    // Start is called before the first frame update
    void Start()
    {
        this.director = GameObject.FindWithTag("FieldDirector") as GameObject;
        this.ws = this.director.GetComponent<ChangeWeather>().WS;

        this.animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeWeather.WeatherState preWS = ws;
        this.ws = director.GetComponent<ChangeWeather>().WS;

        if (preWS != this.ws)
        {
            Func_Rainbow(preWS);
        }
    }

    private void Func_Rainbow(ChangeWeather.WeatherState preWS)
    {
        switch (this.ws)
        {
            default:
                if(Appear/*==true*/)
                {
                    Appear = false;
                    animator.SetBool("Appear", Appear);
                    GetComponent<Collider2D>().isTrigger = true;
                }
                break;

            case ChangeWeather.WeatherState.Sunny:
                if (preWS == ChangeWeather.WeatherState.Rainy ||
                    preWS == ChangeWeather.WeatherState.Rainy_Hard)
                {
                    Appear = true;
                    Invoke("Anim_Rainbow", 1.0f);
                }
                break;

            case ChangeWeather.WeatherState.Sunny_Hard:
                break;
        }
    }

    private void Anim_Rainbow()
    {
        animator.SetBool("Appear", this.Appear);
        Invoke("Move_Collider", 1.1f);
    }
    private void Move_Collider()
    {
        GetComponent<Collider2D>().isTrigger = false;
    }
}