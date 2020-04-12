using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class TitleSelect : MonoBehaviour
{
    //アタッチした全てのボタンを管理する。
    //コントローラーとマウスで同時に押されるのを防ぐ

    //選択状態にする
    public void SelectSelf()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
    //非選択状態にする
    public void NonSelectSelf()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void Change_Select_Num()
    {

    }
}
