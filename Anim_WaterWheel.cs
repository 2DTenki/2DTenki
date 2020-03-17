using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_WaterWheel : MonoBehaviour
{
	public bool AnimFlag_WaterWheel = false;
	public Animator animator;
	private GameObject director;
	public ChangeWeather.WeatherState ws;
	// Start is called before the first frame update
	void Start()
	{
		this.director = GameObject.FindWithTag("FieldDirector");
        this.ws = this.director.gameObject.GetComponent<ChangeWeather>().WS;
        this.animator = this.gameObject.GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		ChangeWeather.WeatherState WS_pre = this.ws;

        this.ws = this.director.gameObject.GetComponent<ChangeWeather>().WS;
		if (WS_pre != this.ws)
		{
			if (this.ws == ChangeWeather.WeatherState.Rainy_Hard)
			{
				AnimFlag_WaterWheel = true;
				Invoke("Move_WaterWheel", 1.5f);
			}
			else
			{
				AnimFlag_WaterWheel = false;
				Invoke("Move_WaterWheel", 0.5f);
			}
		}
	}

	void Move_WaterWheel()
	{
		this.animator.SetBool("WS_RainyHard", AnimFlag_WaterWheel);
	}
}

