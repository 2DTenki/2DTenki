using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherParticle : MonoBehaviour
{
	public ChangeWeather.WeatherState WS;

	public GameObject director;

	public GameObject rainParticle;
	public GameObject snowParticle;
	// Start is called before the first frame update
	void Start()
	{
		this.WS = director.GetComponent<ChangeWeather>().WS;

		Vector2 pos = this.transform.position;

		this.rainParticle = GameObject.Instantiate(rainParticle, pos, Quaternion.identity) as GameObject;
		this.rainParticle.transform.parent = transform;

		this.snowParticle = GameObject.Instantiate(snowParticle, pos, Quaternion.identity) as GameObject;
		this.snowParticle.transform.parent = transform;

	}

	// Update is called once per frame
	void Update()
	{
		Particle();
	}

	private void Particle()
	{

		ChangeWeather.WeatherState preWS = this.WS;
		this.WS = director.GetComponent<ChangeWeather>().WS;

		if (preWS != this.WS)
		{
			switch (this.WS)
			{
				
				case ChangeWeather.WeatherState.Rainy:
					//雨パーティクル始動
					this.rainParticle.GetComponent<ParticleSystem>().Play();
					//他パーティクル止める
					this.snowParticle.GetComponent<ParticleSystem>().Stop();
					break;

				case ChangeWeather.WeatherState.Snowy:
					//雪パーティクル始動
					this.snowParticle.GetComponent<ParticleSystem>().Play();
					//他パーティクル止める
					this.rainParticle.GetComponent<ParticleSystem>().Stop();
					break;

				case ChangeWeather.WeatherState.Sunny:
					//他パーティクル止める
					this.rainParticle.GetComponent<ParticleSystem>().Stop();
					this.snowParticle.GetComponent<ParticleSystem>().Stop();
					break;

				case ChangeWeather.WeatherState.Cloudy:
					this.rainParticle.GetComponent<ParticleSystem>().Stop();
					this.snowParticle.GetComponent<ParticleSystem>().Stop();
					break;
			}
		}
	}
}