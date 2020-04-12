using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Rope : MonoBehaviour
{
    public Animator animator;
    private GameObject waterWheel;
    private bool AnimFlag_RopeMove;
    private bool AnimFlag_RopeFreeze;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = gameObject.GetComponent<Animator>();
        this.waterWheel = GameObject.FindWithTag("WaterWheel");
        this.AnimFlag_RopeMove = this.waterWheel.gameObject.GetComponent<Anim_WaterWheel>().Move;
        this.AnimFlag_RopeFreeze = this.waterWheel.gameObject.GetComponent<Anim_WaterWheel>().Freeze;
    }

    // Update is called once per frame
    void Update()
    {
        //変更前の水車の状態を取得
        bool pre_Move = this.AnimFlag_RopeMove;
        bool pre_Freeze = this.AnimFlag_RopeFreeze;
        //変更後の水車の状態を取得
        this.AnimFlag_RopeMove = this.waterWheel.gameObject.GetComponent<Anim_WaterWheel>().Move;
        this.AnimFlag_RopeFreeze = this.waterWheel.gameObject.GetComponent<Anim_WaterWheel>().Freeze;
        //変更前と後で状態が変わっていた場合のみ行う
        if (this.AnimFlag_RopeMove != pre_Move)
        {
            //ロープ動かす
            if (this.AnimFlag_RopeMove == true)
            {
                Invoke("Move_Rope", 1.5f);
            }
            //ロープ止める
            else
            {
                Invoke("Stop_Rope", 0.5f);
            }
        }
        //変更前と後で状態が変わっていた場合のみ行う
        if (this.AnimFlag_RopeFreeze != pre_Freeze)
        {
            //凍らせる
            if (this.AnimFlag_RopeFreeze == true)
            {
                Invoke("Freeze_Rope", 0.5f);
            }
            //解凍する
            else
            {
                Invoke("Freeze_Rope", 0.5f);
            }
        }
    }

    private void Move_Rope()
    {
        this.animator.SetBool("Move", true);
    }

    private void Stop_Rope()
    {
        this.animator.SetBool("Move", false);
    }

    private void Freeze_Rope()
    {
        this.animator.SetBool("Freeze", this.AnimFlag_RopeFreeze);
    }
}
