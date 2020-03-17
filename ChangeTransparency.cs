using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTransparency : MonoBehaviour
{
    //透明度を変えるスクリプト

    private float alpha;//0～1で指定
    private GameObject item;
    private SpriteRenderer sr;
    private bool ChangeFlag;

    // Start is called before the first frame update
    void Start()
    {
        this.alpha = 1.0f;
        this.item = GetComponent<GameObject>();
        this.sr = GetComponent<SpriteRenderer>();
        this.ChangeFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            this.ChangeFlag = true;
        }

        if (ChangeFlag == true) 
        {
            this.alpha -= Time.deltaTime;
            if (this.alpha <= 0)
            {
                this.alpha = 0;
                this.ChangeFlag = false;
            }
            sr.color = new Color(1, 1, 1, alpha);
            //this.item.GetComponent<Image>().color = new Color(1, 1, 1, alpha);
        }
    }

}
