using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepInRain : MonoBehaviour
{
    public Animator animator;
    private GameObject waterWheel;
    private bool AnimFlag_DoorOpen;
    private bool AnimFlag_DoorFreeze;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = gameObject.GetComponent<Animator>();
        this.waterWheel = GameObject.FindWithTag("WaterWheel");
        this.AnimFlag_DoorOpen = this.waterWheel.gameObject.GetComponent<Anim_WaterWheel>().Move;
        this.AnimFlag_DoorFreeze = this.waterWheel.gameObject.GetComponent<Anim_WaterWheel>().Freeze;
    }

    // Update is called once per frame
    void Update()
    {
        //変更前の水車の状態を取得
        bool pre_Move = this.AnimFlag_DoorOpen;
        bool pre_Freeze = this.AnimFlag_DoorFreeze;
        //変更後の水車の状態を取得
        this.AnimFlag_DoorOpen = this.waterWheel.gameObject.GetComponent<Anim_WaterWheel>().Move;
        this.AnimFlag_DoorFreeze = this.waterWheel.gameObject.GetComponent<Anim_WaterWheel>().Freeze;
        //変更前と後で状態が変わっていた場合のみ行う
        if (this.AnimFlag_DoorOpen != pre_Move)
        {
            //ドア開ける
            if (this.AnimFlag_DoorOpen == true)
            {
                Invoke("Move_DoorOpen", 2.5f);
            }
            //ドア閉める
            else
            {
                Invoke("Stop_Rope", 1.0f);
            }
        }
        //変更前と後で状態が変わっていた場合のみ行う
        if (this.AnimFlag_DoorFreeze != pre_Freeze)
        {
            //凍らせる
            if (this.AnimFlag_DoorFreeze == true)
            {
                Invoke("Freeze_Base", 0.5f);
            }
            //解凍する
            else
            {
                Invoke("Freeze_Base", 0.5f);
            }
        }
    }

    private void Move_DoorOpen()
    {
        this.animator.SetBool("Door_Open", true);
        Invoke("MoveCollider_Door", 2.5f);
    }

    private void Move_DoorClose()
    {
        this.animator.SetBool("Door_Open", false);
        GetComponent<Collider2D>().offset = new Vector2(0, -0.4f);
    }

    private void MoveCollider_Door()
    {
        GetComponent<Collider2D>().offset = new Vector2(0, 1);
    }


    private void Freeze_Door()
    {
        this.animator.SetBool("Freeze", this.AnimFlag_DoorFreeze);
    }
}
