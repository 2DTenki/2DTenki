using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Goal : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //	this.animator.SetTrigger("Goal_Trigger");
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            this.animator.SetTrigger("Goal_Trigger");
        }
    }
}
