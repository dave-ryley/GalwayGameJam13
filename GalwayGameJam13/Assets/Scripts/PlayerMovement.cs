using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    public CircleCollider2D myBodyCollider;
    public Rigidbody2D myRigidBody;
    public Health_Script health;

    public float runSpeed = 40f;
    public Vector2 deathKick = new Vector2(25f, 25f);

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        if(health.alive == false)
        {
            return;
        }
        horizontalMove = transform.position.x + runSpeed;

        animator.SetFloat("Speed", horizontalMove);

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }


    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
    }
}
