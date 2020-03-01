using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
	private GameObject waterWheel;
	private bool AnimFlag_Door;
	public Animator animator;

	// Start is called before the first frame update
	void Start()
    {
		this.animator = gameObject.GetComponent<Animator>();
        this.waterWheel = GameObject.FindWithTag("WaterWheel");
		this.AnimFlag_Door = waterWheel.gameObject.GetComponent<Anim_WaterWheel>().AnimFlag_WaterWheel;
	}

	// Update is called once per frame
	void Update()
    {
        //変更前の天気を取得
		bool MoveFlag = this.AnimFlag_Door;
        //変更後の天気を取得
        this.AnimFlag_Door = waterWheel.gameObject.GetComponent<Anim_WaterWheel>().AnimFlag_WaterWheel;
        //変更前と後で天気が変わっていた場合のみ行う
        if (this.AnimFlag_Door != MoveFlag) 
		{
			if(this.AnimFlag_Door==true)
			{
                Invoke("Move_DoorOpen", 2.5f);
            }
            else
			{
                Invoke("Move_DoorClose", 1.0f);
                //Invoke("Anim_Reset", 2.5f);
            }
        }
	}

    void Move_DoorOpen()
    {
        this.animator.SetBool("Door_Open", true);
        this.animator.SetBool("Door_Close", false);
    }
    void Move_DoorClose()
    {
        this.animator.SetBool("Door_Close", true);
        this.animator.SetBool("Door_Open", false);
    }
    //void Anim_Reset()
    //{
    //    this.animator.SetBool("Door_Open", false);
    //    this.animator.SetBool("Door_Close", false);

    //}
}
