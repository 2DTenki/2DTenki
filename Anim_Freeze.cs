using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Freeze : MonoBehaviour
{
    public Animator animator;
    private GameObject waterWheel;
    private bool Freeze_obj;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = gameObject.GetComponent<Animator>();
        this.waterWheel = GameObject.FindWithTag("WaterWheel");
        this.Freeze_obj = this.waterWheel.gameObject.GetComponent<Anim_WaterWheel>().Freeze;
    }

    // Update is called once per frame
    void Update()
    {
        //変更前の水車の状態を取得
        bool pre_Freeze = this.Freeze_obj;
        //変更後の水車の状態を取得
        this.Freeze_obj = this.waterWheel.gameObject.GetComponent<Anim_WaterWheel>().Freeze;
        //変更前と後で状態が変わっていた場合のみ行う
        if (this.Freeze_obj != pre_Freeze)
        {
            //凍らせる
            if (this.Freeze_obj == true)
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

    private void Freeze_Base()
    {
        this.animator.SetBool("Freeze", this.Freeze_obj);
    }
}
