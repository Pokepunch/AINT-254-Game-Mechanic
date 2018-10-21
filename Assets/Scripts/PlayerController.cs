using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // The Player's Rigidbody component
    Rigidbody body;

    // The Player's charge, used for the jumping mechanic
    private int charge;
    public int chargeMax = 70;

    private int dashCooldown;
    private int dashCooldownMax = 120;

    public float speed = 200;

    private bool isOnGround;
    private bool wasOnGround;

    public float jumpForce = 900;
    private bool isJumping = false;

    public float dashForce = 1800;
    private bool isDashing = false;

    public Vector2 movement;

	// Use this for initialization
	void Start ()
    {
        body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetJump();
        GetDashCharge();
    }

    private void FixedUpdate()
    {
        CheckGround();
        GetInput();
        DoHorizMovement();
    }

    public void GetInput()
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

    private void DoHorizMovement()
    {
        if (movement.x != 0 && !isDashing)
        {
            body.AddForce(movement.x * Vector2.right * speed);
        }
    }

    private void GetDashCharge()
    {
        if (Input.GetButton("ChargeDash") && dashCooldown != 0)
        {
            if (charge < chargeMax)
            {
                charge += 10;
            }
        }
        else if (Input.GetButtonUp("ChargeDash"))
        {
            if (charge <= chargeMax)
            {
                GetInput();
                isDashing = true;
                body.AddForce(new Vector3(movement.x * dashForce, movement.y * dashForce, 0));
            }
            charge = 0;
        }
        else
        {
            charge = 0;
        }
    }

    private void GetJump()
    {
        if (Input.GetButtonDown("Jump") && !isJumping && !isDashing)
        {
            isJumping = true;
            body.AddForce(new Vector3(0, jumpForce, 0));
            charge = 0;
        }
    }

    public void CheckGround()
    {
        wasOnGround = isOnGround;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.5f))
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }
        if (!wasOnGround && isOnGround && isJumping)
        {
            isJumping = false;
            isDashing = false;
        }
        if (isDashing && isOnGround)
        {
            isDashing = false;
            if (dashCooldown < dashCooldownMax)
            {
                dashCooldown++;
            }
            else
            {
                dashCooldown = 0;
            }
        }
    }
}
