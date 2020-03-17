using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMap : MonoBehaviour
{
    public SpriteRenderer MainSpriteRenderer;
    // publicで宣言し、inspectorで設定可能にする
    public Sprite CloudySprite;
    public Sprite RainSprite;
    public Sprite SnowSprite;
    public Sprite SunnySprite;

	private GameObject director;
	private ChangeWeather.WeatherState WS;
	
    void Start()
    {
        // このobjectのSpriteRendererを取得
        this.MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //MainSpriteRenderer.sprite = CloudySprite;
        this.director = GameObject.FindWithTag("FieldDirector");
        this.WS = director.GetComponent<ChangeWeather>().WS;		
    }

     void Update()
    {
		//Cloudy,         //曇り（ベース）
		//Rainy,          //雨
		//Rainy_Hard,     //雨（強）
		//Sunny,          //晴れ
		//Sunny_Hard,     //晴れ（強）
		//Snowy,          //雪
		//Snowy_Hard,     //雪(強)

		ChangeWeather.WeatherState preWS = WS;
		this.WS = this.director.GetComponent<ChangeWeather>().WS;

		if (preWS != this.WS)
		{
			switch (this.WS)
			{
				case ChangeWeather.WeatherState.Rainy:
					Invoke("rain", 3.0f);
					break;

				case ChangeWeather.WeatherState.Snowy:
					Invoke("snow", 3.0f);
					break;

				case ChangeWeather.WeatherState.Sunny:
					Invoke("sunny", 3.0f);
					break;

				case ChangeWeather.WeatherState.Cloudy:
					
					break;
			}
		}
    }
    void rain()
    {
        MainSpriteRenderer.sprite = RainSprite;
    }
    void snow()
    {
        MainSpriteRenderer.sprite = SnowSprite;
    }
    void sunny()
    {
        MainSpriteRenderer.sprite = SunnySprite;
    }
}
