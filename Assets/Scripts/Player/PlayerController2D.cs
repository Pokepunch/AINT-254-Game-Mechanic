using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour {


    CharacterController controller;

    public static bool controlLocked = false;

    private int isLeft = 0;
    public int IsLeft
    {
        get { return isLeft; }
        set { isLeft = value; }
    }

    public Material red;
    public Material blue;

    public float speed = 200;
    private float speedBack;

    // The Player's charge, used for the dashing mechanic
    private float charge;
    public float chargeDelay = 1.5f;

    public float jumpForce = 2;
    private float jumpForceCurrent;
    public float jumpHeight;
    private bool isJumping = false;

    private bool jumpButtonDown = false;

    public float dashForce = 1800;
    private bool isDashing = false;
    public bool IsDashing
    {
        get { return isDashing; }
        set { isDashing = value; }
    }
    private bool canDash = true;

    private bool isGrounded = true;
    public Transform groundChecker;
    public LayerMask ground;

    public float dashTime;
    public static float dashTimeBack;
    public static bool isDashInfinite = false;

    public Vector2 movement;
    public Vector3 moveDirection =  Vector3.zero;

    public float gravity = 0.6f;

    // Use this for initialization
    void Start ()
    {
        controller = GetComponent<CharacterController>();
        speedBack = speed;
        dashTimeBack = dashTime;
    }

    private void Update()
    {
        CheckGround();
        GetInput();
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
                    x = isLeft = 1;
                }
                else if (x < 0)
                {
                    x = isLeft = -1;
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
                    y = 0;
                }
            }
            movement = new Vector2(x, y);
        }
    }

    private void GetJump()
    {
        if (Input.GetButtonDown("Jump") && !isJumping && isGrounded)
        {
            isJumping = true;
            jumpButtonDown = true;
            jumpForceCurrent = 5;
        }
        else if (Input.GetButtonUp("Jump") && isJumping)
        {
            jumpButtonDown = false;
        }
        if (jumpButtonDown && isJumping)
        {
            if (jumpForceCurrent < jumpForce + jumpHeight)
            {
                jumpForceCurrent += 1;
                moveDirection.y = jumpForceCurrent;
            }
        }
    }

    private void GetDash()
    {
        if (isDashing)
        {
            if (dashTime > 0)
            {
                if (movement == Vector2.zero && !controlLocked)
                {
                    movement.x = isLeft;
                }
                moveDirection = new Vector3(movement.x * dashForce, movement.y * dashForce, 0);
                if (!isDashInfinite)
                {
                    dashTime -= Time.deltaTime;
                }
            }
            else
            {
                isDashing = false;
                canDash = false;
                moveDirection.y = moveDirection.y / 2;
                GetComponent<Renderer>().material = red;
                dashTime = dashTimeBack;
                GetInput();
                speed = dashForce;
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
                else
                {
                    GetComponent<Renderer>().material = blue;
                }
            }
            else if (Input.GetButtonUp("ChargeDash"))
            {
                if (charge >= chargeDelay)
                {
                    isDashing = true;
                    isJumping = false;
                }
                charge = 0;
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
        isGrounded = Physics.CheckSphere(groundChecker.position, 0.1f, ground, QueryTriggerInteraction.Ignore);
        if (isGrounded)
        {
            canDash = true;
            speed = speedBack;
            jumpForceCurrent = 0;
            moveDirection.y = 0;
            isJumping = false;
        }
        else
        {
            if (!isDashing)
            {
                moveDirection.y += gravity;
                if (moveDirection.y < -40)
                {
                    moveDirection.y = -40;
                }
            }
        }
    }
}
