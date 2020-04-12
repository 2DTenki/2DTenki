using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScale : MonoBehaviour
{
    //デフォルトのアスペクト比を設定
    public float defaultWidth = 16.0f;
    public float defaultHeight = 9.0f;
    void Start()
    {
        //HierarchyのMain Cameraをcamera.mainで取得して変数に格納
        Camera mainCamera = Camera.main;

        //デフォルトのアスペクト比 
        float defaultAspect = defaultWidth / defaultHeight;
   
       //デフォルトの画面とunityの画面のアスペクト比
        float NewAspect = (float)Screen.width / (float)Screen.height;

        //デフォルトとunity画面の比率
        float CameraSaiz = NewAspect / defaultAspect;

        //サイズ調整
        mainCamera.orthographicSize /= CameraSaiz;
                    //画面下半分
    }
}
