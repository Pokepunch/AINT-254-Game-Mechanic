using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // The Player's Rigidbody component
    Rigidbody body;

    // The Player's charge, used for the jumping mechanic
    private int charge;
    public int chargeMax = 70;

    // Variables used to delay the start of the charge build up
    private int chargeDelay = 0;
    public int chargeDelayTarget = 60;

    public float velocityClamp = 30;

    public float maxSpeed = 15;
    public float speed = 20;

	// Use this for initialization
	void Start ()
    {
        body = GetComponent<Rigidbody>();
        body.freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetJumpCharge();
	}

    private void FixedUpdate()
    {
        DoMovement();
    }

    private void DoMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (body.velocity.magnitude <= maxSpeed)
        {
            body.AddRelativeForce(new Vector3(x * speed, 0, z *speed));
        }
    }

    private void GetJumpCharge()
    {
        if (Input.GetButton("Jump"))
        {
            if (chargeDelay < chargeDelayTarget)
            {
                chargeDelay++;
            }
            else
            {
                if (charge < chargeMax)
                {
                    charge += 10;
                    Debug.Log("Charge = " + charge);
                }
                else
                {
                    Debug.Log("Charge is max");
                }
            }
        }
        else
        {
            chargeDelay = 0;
        }
        if (Input.GetButtonUp("Jump"))
        {
            body.AddForce(new Vector3(0, 500 + charge, 0));
            charge = 0;
        }
    }
}
