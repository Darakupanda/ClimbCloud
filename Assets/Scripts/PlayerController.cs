using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TreeEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigig2D;
    Animator animator;
    float jumpForce = 680.0f;
    float walkForce = 30.0f;
    float maxWalkSpeed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate=60;
        this.rigig2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) &&
            this.rigig2D.velocity.y == 0){
            this.animator.SetTrigger("JumpTrigger");
            this.rigig2D.AddForce(transform.up * this.jumpForce);
        }

        int key = 0;
        if(Input.GetKey(KeyCode.RightArrow))key = 1;
        if(Input.GetKey(KeyCode.LeftArrow))key= -1;
        float speedx = Mathf.Abs(this.rigig2D.velocity.x);

        if(speedx < this.maxWalkSpeed){
            this.rigig2D.AddForce(transform.right * key * this.walkForce);
        }

        if(key != 0){
            transform.localScale = new Vector3(key,1,1);
        }

        if(this.rigig2D.velocity.y == 0){
            this.animator.speed = speedx/2.0f;
        }else {
            this.animator.speed = 1.0f;
        }

        if(transform.position.y < -10){
            SceneManager.LoadScene("GameScenes");
        }
    }
    void OnTriggerEnter2D(Collider2D collision){
        Debug.Log("ゴール");
        SceneManager.LoadScene("ClearScene");
    }

}
