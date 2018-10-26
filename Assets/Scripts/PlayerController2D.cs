using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour {


    CharacterController controller;

    public float speed = 200;
    private float speedBack;

    // The Player's charge, used for the dashing mechanic
    private float charge;
    public float chargeDelay = 1.5f;

    private bool isOnGround;
    private bool wasOnGround;

    public float jumpForce = 2;
    private float jumpForceCurrent;
    private bool isJumping = false;

    public float dashForce = 1800;
    private bool isDashing = false;
    private bool canDash = true;

    public float dashTime;
    private float dashTimeBack;

    public Vector2 movement;
    public Vector3 moveDirection =  Vector3.zero;

    // Use this for initialization
    void Start ()
    {
        controller = GetComponent<CharacterController>();
        speedBack = speed;
        dashTimeBack = dashTime;
    }

    private void Update()
    {
        GetInput();
        CheckGround();
        GetDash();
        if (!isDashing)
        {
            GetJump();
            DoHorizMovement();
        }
        controller.Move(moveDirection * Time.deltaTime);
    }

    public void GetInput()
    {
        if (!isDashing)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            if (Input.GetButton("Horizontal"))
            {
                if (x > 0)
                {
                    x = 1;
                }
                else if (x < 0)
                {
                    x = -1;
                }
            }
            if (Input.GetButton("Vertical"))
            {
                if (y > 0)
                {
                    y = 1;
                }
                else if (y < 0)
                {
                    y = -1;
                }
            }
            movement = new Vector2(x, y);
        }
    }

    private void GetJump()
    {
        if (Input.GetButtonDown("Jump") && !isJumping && controller.isGrounded)
        {
            jumpForceCurrent = jumpForce;
            isJumping = true;
        }
        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpForceCurrent < jumpForce + 24f)
            {
                moveDirection.y = jumpForceCurrent;
                jumpForceCurrent += 1;
            }
        }
    }

    private void GetDash()
    {
        if (isDashing)
        {
            if (dashTime > 0)
            {
                moveDirection = new Vector3(movement.x * dashForce, movement.y * dashForce, 0);
                dashTime -= Time.deltaTime;
            }
            else
            {
                isDashing = false;
                canDash = false;
                dashTime = dashTimeBack;
                GetInput();
                /*
                if (moveDirection.x < 0)
                {
                    speed = -moveDirection.x;
                }
                else if(moveDirection.x > 0)
                {
                    speed = moveDirection.x;
                }
                */
            }
        }
        else if (canDash)
        {
            if (Input.GetButton("ChargeDash"))
            {
                if (charge < chargeDelay)
                {
                    charge += Time.deltaTime;
                }
            }
            else if (Input.GetButtonUp("ChargeDash"))
            {
                if (charge >= chargeDelay)
                {
                    isDashing = true;
                    charge = 0;
                }
            }
        }
    }

    private void DoHorizMovement()
    {
        if (movement.x != 0)
        {
            moveDirection.x = movement.x * speed;
        }
        else
        {
            moveDirection.x = 0;
        }
    }

    public void CheckGround()
    {
        if (controller.isGrounded)
        {
            isJumping = false;
            canDash = true;
            speed = speedBack;
            moveDirection.y = -controller.stepOffset / Time.deltaTime;
            jumpForceCurrent = 0;
        }
        else
        {
            if (!isDashing)
            {
                moveDirection.y += Physics.gravity.y;
                if (moveDirection.y < -35)
                {
                    moveDirection.y = -35;
                }
            }
        }
    }
}
