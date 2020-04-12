using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public ChangeWeather.WeatherState ws;
    private GameObject director;
    public GameObject SunnyParticle;
    public GameObject SunnyHardParticle;

    public Animator animator;
    private bool IsFreeze;
    private bool IsMelt;

    // Start is called before the first frame update
    void Start()
    {
        this.director = GameObject.FindWithTag("FieldDirector") as GameObject;
        this.ws = this.director.GetComponent<ChangeWeather>().WS;

        this.animator = gameObject.GetComponent<Animator>();
        this.IsFreeze = false;
        this.IsMelt = true;

        Particle_Start();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeWeather.WeatherState preWS = ws;
        ws = director.GetComponent<ChangeWeather>().WS;

        if (preWS != ws)
        {
            Animation_Mirror();
            Particle_Update();
        }
    }

    //--------------------------------------------------------------------------------------------------------------
    //パーティクルの準備
    private void Particle_Start()
    {
        Vector2 pos = new Vector2(this.transform.position.x + 0.7f, this.transform.position.y - 0.1f);

        this.SunnyParticle = GameObject.Instantiate(this.SunnyParticle, pos, Quaternion.identity) as GameObject;
        this.SunnyParticle.transform.parent = transform;

        this.SunnyHardParticle = GameObject.Instantiate(this.SunnyHardParticle, pos, Quaternion.identity) as GameObject;
        this.SunnyHardParticle.transform.parent = transform;

        this.SunnyParticle.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
        this.SunnyHardParticle.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
    }

    //パーティクルの更新
    private void Particle_Update()
    {
        if (IsFreeze == false)
        {
            switch (this.ws)
            {
                //下のcaseにかからなかったらパーティクルを切る
                default:
                    this.SunnyParticle.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
                    this.SunnyHardParticle.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
                    break;

                case ChangeWeather.WeatherState.Sunny:
                    //晴れパーティクル(レーザーのやつ)始動
                    this.SunnyParticle.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                    //晴れ強パーティクル止める
                    this.SunnyHardParticle.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
                    break;

                case ChangeWeather.WeatherState.Sunny_Hard:
                    //鏡が解けていたら光を生成する
                    if (IsMelt == true)
                    {
                        SunnyHardParticle_Update();
                    }
                    else
                    {
                        IsMelt = true;
                        Invoke("SunnyHardParticle_Update", 2.5f);
                    }
                    break;
            }
        }
    }
    private void SunnyHardParticle_Update()
    {
        //晴れ強パーティクル(レーザーのやつ)始動
        this.SunnyHardParticle.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        //晴れパーティクル止める
        this.SunnyParticle.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
    }
    private void Animation_Mirror()
    {
        if (this.ws == ChangeWeather.WeatherState.Snowy_Hard)
        {
            this.IsFreeze = true;
            this.IsMelt = false;
            Invoke("AnimM", 0.5f);
        }
        else if (this.ws == ChangeWeather.WeatherState.Sunny_Hard)
        {
            this.IsFreeze = false;
            Invoke("AnimM", 0.5f);
        }
    }
    //時間差用
    private void AnimM()
    {
        this.animator.SetBool("IsFreeze", IsFreeze);
    }
}

//「パッ」って変わる方
//this.SunnyParticle.SetActive(false);
//this.SunnyHardParticle.SetActive(false);