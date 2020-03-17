﻿using System.Collections;
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
        public bool ClearFlag;      //クリアフラグ(Goal.csで変更)
        public enum Angle           //キャラ向き
        {
            Right,
            Left,
        }

        public Angle angle;

        public Rigidbody2D rigid2D; //プレイヤの物理

        public Chara(float x, float y, float s, float jF, bool c, Angle a, Rigidbody2D rigidbody2D)
        {
            this.cx = x;
            this.cy = y;
            this.walkSpeed = s;
            this.jumpForce = jF;
            this.angle = a;
            this.ClearFlag = c;
            this.rigid2D = rigidbody2D;
        }
    }

    public Chara player;

    // Start is called before the first frame update
    void Start()
    {
        //X,Y,歩行速度,ジャンプ力,クリアフラグ,キャラ向き,Rigidbody2Dを取得
        this.player =
            new Chara(0, 0, 5.0f, 550.0f, false, Chara.Angle.Right, GetComponent<Rigidbody2D>());
    }

    // Update is called once per frame
    void Update()
    {
        if (this.player.ClearFlag == false)
        {
            Player_Move();
        }
    }

    //-------------------------------------------------------------------------------------
    //プレイヤの動き
    void Player_Move()
    {
        if (Input.GetButton("Fire1") == true)
        {
            Stop_SideMove();
        }
        else
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

            //キャラ向き変更
            //移動量を取得
            if (this.player.cx > 0)
            {
                this.player.angle = Chara.Angle.Right;
            }
            else if (this.player.cx < 0)
            {
                this.player.angle = Chara.Angle.Left;
            }
            //右
            if (this.player.angle == Chara.Angle.Right)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            //左
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            this.player.rigid2D.velocity = new Vector2(this.player.cx, this.player.cy);
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
