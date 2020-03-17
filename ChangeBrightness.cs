using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBrightness : MonoBehaviour
{
    //明るさを変えるスクリプト

    private GameObject director;
    private ChangeWeather.WeatherState ws;
    private new Light light;

    // Start is called before the first frame update
    void Start()
    {
        this.director = GameObject.FindWithTag("FieldDirector");
        this.ws = this.director.GetComponent<ChangeWeather>().WS;
        this.light = GetComponent<Light>();
        light.intensity = 0.6f;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeWeather.WeatherState preWS = this.ws;
        this.ws = this.director.GetComponent<ChangeWeather>().WS;

        if (preWS != ws)
        {
            SetBrightness(ws);
        }
    }

    void SetBrightness(ChangeWeather.WeatherState WS)
    {
        switch (WS)
        {
            case ChangeWeather.WeatherState.Cloudy:
                light.intensity = 0.6f;
                break;

            case ChangeWeather.WeatherState.Sunny:
                light.intensity = 0.8f;
                break;

            case ChangeWeather.WeatherState.Sunny_Hard:
                light.intensity = 1.0f;
                break;

            case ChangeWeather.WeatherState.Rainy:
                light.intensity = 0.5f;
                break;

            case ChangeWeather.WeatherState.Rainy_Hard:
                light.intensity = 0.4f;
                break;

            case ChangeWeather.WeatherState.Snowy:
                light.intensity = 0.5f;
                break;

            case ChangeWeather.WeatherState.Snowy_Hard:
                light.intensity = 0.4f;
                break;
        }
    }
}
