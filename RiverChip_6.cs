using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverChip_6 : MonoBehaviour
{
    public SpriteRenderer MainSpriteRenderer;
    // publicで宣言し、inspectorで設定可能にする
    public Sprite rake;
    public Sprite ice;


    private GameObject director;
    public ChangeWeather.WeatherState ws;

    void Start()
    {
        // このobjectのSpriteRendererを取得
        MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //MainSpriteRenderer.sprite = CloudySprite;
        director = GameObject.FindWithTag("FieldDirector");
        ws = director.gameObject.GetComponent<ChangeWeather>().WS;
    }

    void Update()
    {
        ChangeWeather.WeatherState WS_pre = this.ws;

        ws = director.gameObject.GetComponent<ChangeWeather>().WS;

        if (WS_pre != this.ws)
        {
            switch (this.ws)
            {
                case ChangeWeather.WeatherState.Snowy_Hard:
                    Invoke("iceChange", 0.2f);
                    break;
                case ChangeWeather.WeatherState.Sunny_Hard:
                    MainSpriteRenderer.sprite = rake;
                    break;
            }
        }
    }
    void iceChange()
    {
        MainSpriteRenderer.sprite = ice;
    }

}
