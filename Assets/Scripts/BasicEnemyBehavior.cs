using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyBehavior : MonoBehaviour
{

    public float travelDistance;
    public float travelSpeed;

    // 0 = left, 1 = right
    int direction = 0;
    float startPos;

	// Use this for initialization
	void Start ()
    {
        startPos = transform.position.x;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (direction == 0)
        {
            if (transform.position.x <= startPos - travelDistance)
            {
                direction = 1;
            }
            else
            {
                transform.position = new Vector3(transform.position.x - travelSpeed, transform.position.y, transform.position.z);
            }
        }
        else
        {
            if (transform.position.x >= startPos + travelDistance)
            {
                direction = 0;
            }
            else
            {
                transform.position = new Vector3(transform.position.x + travelSpeed, transform.position.y, transform.position.z);
            }
        }
	}
}
