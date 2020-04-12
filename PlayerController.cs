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
        public int changeCnt;      //天候変化できる回数(ChangeWeather.csで操作)
        public enum Angle          //キャラ向き
        {
            Right,
            Left,
        }

        public Angle angle;

        public Rigidbody2D rigid2D; //プレイヤの物理

        public Chara(float x, float y, float ws, float jF, int cc, Angle a, Rigidbody2D rigidbody2D)
        {
            this.cx = x;
            this.cy = y;
            this.walkSpeed = ws;
            this.jumpForce = jF;
            this.changeCnt = cc;
            this.angle = a;
            this.rigid2D = rigidbody2D;
        }
    }

    public Chara player;
    private GameManagement gameManegement;

    // Start is called before the first frame update
    void Start()
    {
        this.player =
            new Chara(
            0,                          //X
            0,                          //Y
            5.0f,                       //歩行速度
            550.0f,                     //ジャンプ力
            100,                         //天候変化できる回数(ステージ1の回数[2以降はCreatMap_FromTxtで設定])
            Chara.Angle.Right,          //キャラ向き
            GetComponent<Rigidbody2D>() //Rigidbody2Dを取得
                    );

        this.gameManegement = GameObject.FindWithTag("GameManager").GetComponent<GameManagement>();
        this.gameManegement.ClearFlag = false;
        this.gameManegement.GameOverFlag = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameManegement.ClearFlag == false && this.gameManegement.GameOverFlag == false)
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
