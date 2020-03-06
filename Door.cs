using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
	public Animator animator;
	private GameObject waterWheel;
    private Collider2D col;
	private bool AnimFlag_DoorOpen;

	// Start is called before the first frame update
	void Start()
    {
		this.animator = gameObject.GetComponent<Animator>();
        this.waterWheel = GameObject.FindWithTag("WaterWheel");
        col = GetComponent<Collider2D>();
		this.AnimFlag_DoorOpen = waterWheel.gameObject.GetComponent<Anim_WaterWheel>().AnimFlag_WaterWheel;
	}

	// Update is called once per frame
	void Update()
    {
        //変更前の水車の状態を取得
		bool pre = this.AnimFlag_DoorOpen;
        //変更後の水車の状態を取得
        this.AnimFlag_DoorOpen = waterWheel.gameObject.GetComponent<Anim_WaterWheel>().AnimFlag_WaterWheel;
        //変更前と後で天気が変わっていた場合のみ行う
        if (this.AnimFlag_DoorOpen != pre) 
		{
            //ドア開ける
			if(this.AnimFlag_DoorOpen==true)
			{
                Invoke("Move_DoorOpen", 2.5f);
            }
            //ドア閉める
            else
            {
                Invoke("Move_DoorClose", 1.0f);
            }
        }
	}

    void Move_DoorOpen()
    {
        this.animator.SetBool("Door_Open", true);
        this.animator.SetBool("Door_Close", false);
        Invoke("IsTrigger_Door", 1.5f);
    }
    void Move_DoorClose()
    {
        this.animator.SetBool("Door_Close", true);
        this.animator.SetBool("Door_Open", false);
        GetComponent<Collider2D>().isTrigger = false;
    }

    void IsTrigger_Door()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
    //void Anim_Reset()
    //{
    //    this.animator.SetBool("Door_Open", false);
    //    this.animator.SetBool("Door_Close", false);

    //}
}
