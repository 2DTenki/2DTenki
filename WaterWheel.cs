using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWheel : MonoBehaviour
{
    public float rotspeed = 0; //回転速度
	ChangeMap Ch;
    //rain ame;
    // Start is called before the first frame update
    void Start()
    {
		//  ame = GameObject.Find("rain").GetComponent<rain>();
		Ch = GameObject.Find("map_0Prefab").GetComponent<ChangeMap>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Ch.MainSpriteRenderer.sprite == Ch.RainSprite)
        {
            rotspeed = 1f;
            transform.Rotate(0, 0, rotspeed);
            rotspeed *= 1.98f;
        }
    }
    
}
