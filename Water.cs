using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private GameObject director;
    public ChangeWeather.WeatherState ws;
    public GameObject water;
    public GameObject waterHard;

    // Start is called before the first frame update
    void Start()
    {
        this.director = GameObject.FindWithTag("FieldDirector");
        this.ws = this.director.gameObject.GetComponent<ChangeWeather>().WS;
        Vector2 pos = new Vector2(transform.position.x - 1.8f, transform.position.y + 2.45f);

        this.water = GameObject.Instantiate(this.water, pos, Quaternion.identity) as GameObject;
        this.water.transform.parent = transform;

        this.waterHard = GameObject.Instantiate(this.waterHard, pos, Quaternion.identity) as GameObject;
        this.waterHard.transform.parent = transform;

        this.water.GetComponent<ParticleSystem>().Stop();
        this.waterHard.GetComponent<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeWeather.WeatherState preWS = this.ws;
        this.ws = this.director.gameObject.GetComponent<ChangeWeather>().WS;

        if (preWS != this.ws)
        {
            switch (this.ws)
            {
                case ChangeWeather.WeatherState.Rainy:
                    //流水パーティクル開始
                    if (preWS != ChangeWeather.WeatherState.Rainy_Hard)
                    {
                        Invoke("Particle", 2.0f);
                    }
                    //直前が雨（強）ならすぐに行う
                    else
                    {
                        this.water.GetComponent<ParticleSystem>().Play();
                        //強い流水パーティクルを無効化
                        this.waterHard.GetComponent<ParticleSystem>().Stop();
                    }
                    break;

                case ChangeWeather.WeatherState.Rainy_Hard:
                    //強い流水パーティクル開始
                    this.waterHard.GetComponent<ParticleSystem>().Play();
                    //流水パーティクルを無効化
                    this.water.GetComponent<ParticleSystem>().Stop();
                    break;

                default:
                    this.water.GetComponent<ParticleSystem>().Stop();
                    this.waterHard.GetComponent<ParticleSystem>().Stop();
                    break;
            }
        }
    }

    private void Particle()
    {
        this.water.GetComponent<ParticleSystem>().Play();
    }
}
