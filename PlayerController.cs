using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	//キャラ
	public struct Chara
	{
		public float cx;            //プレイヤX
		public float cy;            //プレイヤY
		public float walkSpeed;     //歩行速度
		public float jumpForce;     //ジャンプ力

		public Rigidbody2D rigid2D; //プレイヤの物理

		public Chara(float x, float y, float s, float jF, Rigidbody2D rigidbody2D)
		{
			cx = x;
			cy = y;
			walkSpeed = s;
			jumpForce = jF;
			this.rigid2D = rigidbody2D;
		}
	}

	public Chara player;

	// Start is called before the first frame update
	void Start()
	{
		//X,Y,歩行速度,ジャンプ力,Rigidbody2Dを取得
		this.player = new Chara(0, 0, 5.0f, 550.0f, GetComponent<Rigidbody2D>());

	}

	// Update is called once per frame
	void Update()
	{
		Player_Move();

	}

	//-------------------------------------------------------------------------------------
	//プレイヤの動き
	void Player_Move()
	{

		if (Input.GetButton("Fire1") == false)
		{
			//プレイヤ歩行
			this.player.cx = Input.GetAxis("Horizontal") * this.player.walkSpeed;
			if (Input.GetAxis("Horizontal") <= 0.1f && Input.GetAxis("Horizontal") >= -0.1f)
			{
				this.player.cx = 0.0f;

			}

			//プレイヤジャンプ
			if (Input.GetKeyDown(KeyCode.Z) && Velocity_Y(this.player.rigid2D.velocity.y))
			{
				this.player.rigid2D.AddForce(transform.up * this.player.jumpForce);
			}  //↑値は同じ(はず？)↓
			this.player.cy = this.player.rigid2D.velocity.y;


			//移動
			//x
			//this.transform.position += new Vector3(this.player.cx, 0, 0); //軽い
			//this.transform.Translate(this.player.cx, 0, 0);               //若干重い
			//y 
			//this.player.rigid2D.velocity = new Vector2(0, this.player.cy);//Yのみ（Xと分けた場合）

			this.player.rigid2D.velocity = new Vector2(this.player.cx, this.player.cy);//両方（多分重い）

		}
	}
	//-----------------------------------------------------------------------------------------------
	//極小のvelocity.yの変動を無効(if文内で使用)
	bool Velocity_Y(float y)
	{
		if (this.player.rigid2D.velocity.y >= -0.01f && this.player.rigid2D.velocity.y <= 0.01f)
		{
			return true;
		}
		return false;
	}

	//横移動停止(慣性なし)
	void Stop_SideMove()
	{
		this.player.rigid2D.velocity = new Vector2(0, this.player.rigid2D.velocity.y);
	}

}
