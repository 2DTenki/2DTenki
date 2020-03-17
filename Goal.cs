using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Goal : MonoBehaviour
{
    GameManagement gameManegement;
    GameObject player;
    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        gameManegement= GameObject.FindWithTag("GameManager").GetComponent<GameManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            player = GameObject.FindWithTag("Player");
            playerController = player.GetComponent<PlayerController>();
            playerController.player.ClearFlag = true;
            gameManegement.NextStage();
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
