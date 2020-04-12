using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private GameManagement gameManegement;
    private GameObject player;
    private bool ClearFlag, GameOverFlag;
    private float timeCnt;

    // Start is called before the first frame update
    void Start()
    {
        this.gameManegement = GameObject.FindWithTag("GameManager").GetComponent<GameManagement>();
        timeCnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManegement.ClearFlag == true)
        {
            timeCnt += Time.deltaTime;
            if (Input.anyKeyDown && timeCnt >= 1.0f)
            {
                gameManegement.NextStage();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManegement.ClearFlag = true;
        }
    }

    //public void Destroy_AllObject()
    //{
    //    //typeofで指定した型の全てのオブジェクトをforeachで処理
    //    foreach(GameObject obj 
    //        in UnityEngine.Resources.FindObjectsOfTypeAll(typeof(GameObject)))
    //    {
    //        Destroy(obj);
    //    }
    //}
}
