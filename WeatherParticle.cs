using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherParticle : MonoBehaviour
{
	public ChangeWeather.WeatherState WS;

	private GameObject director;

	public GameObject rainParticle;
	public GameObject rainHardParticle;
	public GameObject snowParticle;
	public GameObject snowHardParticle;

	// Start is called before the first frame update
	void Start()
	{
		director = GameObject.Find("Director") as GameObject;
		this.WS = director.GetComponent<ChangeWeather>().WS;

		Vector2 pos = this.transform.position;

		this.rainParticle = GameObject.Instantiate(rainParticle, pos, Quaternion.identity) as GameObject;
		this.rainParticle.transform.parent = transform;

		this.rainHardParticle = GameObject.Instantiate(rainHardParticle, pos, Quaternion.identity) as GameObject;
		this.rainHardParticle.transform.parent = transform;

		this.snowParticle = GameObject.Instantiate(snowParticle, pos, Quaternion.identity) as GameObject;
		this.snowParticle.transform.parent = transform;

		this.snowHardParticle = GameObject.Instantiate(snowHardParticle, pos, Quaternion.identity) as GameObject;
		this.snowHardParticle.transform.parent = transform;
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
					this.rainHardParticle.GetComponent<ParticleSystem>().Stop();
					this.snowParticle.GetComponent<ParticleSystem>().Stop();
					this.snowHardParticle.GetComponent<ParticleSystem>().Stop();
					break;

				case ChangeWeather.WeatherState.Rainy_Hard:
					//大雨パーティクル始動
					this.rainHardParticle.GetComponent<ParticleSystem>().Play();
					//他パーティクル止める
					this.rainParticle.GetComponent<ParticleSystem>().Stop();
					this.snowParticle.GetComponent<ParticleSystem>().Stop();
					this.snowHardParticle.GetComponent<ParticleSystem>().Stop();
					break;

				case ChangeWeather.WeatherState.Snowy:
					//雪パーティクル始動
					this.snowParticle.GetComponent<ParticleSystem>().Play();
					//他パーティクル止める
					this.rainParticle.GetComponent<ParticleSystem>().Stop();
					this.rainHardParticle.GetComponent<ParticleSystem>().Stop();
					this.snowHardParticle.GetComponent<ParticleSystem>().Stop();
					break;

				case ChangeWeather.WeatherState.Snowy_Hard:
					//大雪パーティクル始動
					this.snowHardParticle.GetComponent<ParticleSystem>().Play();
					//他パーティクル止める
					this.rainParticle.GetComponent<ParticleSystem>().Stop();
					this.rainHardParticle.GetComponent<ParticleSystem>().Stop();
					this.snowParticle.GetComponent<ParticleSystem>().Stop();
					break;

				case ChangeWeather.WeatherState.Sunny:
					//他パーティクル止める
					this.rainParticle.GetComponent<ParticleSystem>().Stop();
					this.snowParticle.GetComponent<ParticleSystem>().Stop();
					this.rainHardParticle.GetComponent<ParticleSystem>().Stop();
					this.snowHardParticle.GetComponent<ParticleSystem>().Stop();
					break;

				case ChangeWeather.WeatherState.Cloudy:
					this.rainParticle.GetComponent<ParticleSystem>().Stop();
					this.snowParticle.GetComponent<ParticleSystem>().Stop();
					this.rainHardParticle.GetComponent<ParticleSystem>().Stop();
					this.snowHardParticle.GetComponent<ParticleSystem>().Stop();
					break;
			}
		}
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{

	}
}