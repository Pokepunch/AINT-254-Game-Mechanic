using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour {

    public GameObject dot;
    private GameObject[] dotArray;

    public float force = 50;

    Camera cam;

    private Vector3 direction;

	// Use this for initialization
	void Start ()
    {
        cam = Camera.main;
        dotArray = new GameObject[10];
        for (int i = 0; i < dotArray.Length; i++)
        {
            GameObject temp = Instantiate(dot);
            temp.SetActive(false);
            dotArray[i] = temp;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 projectilePos = cam.WorldToScreenPoint(transform.position);
            projectilePos.z = 0;

            direction = (Input.mousePosition - projectilePos).normalized;
            Debug.Log(direction);
            AimDots();
        }
	}

    private void AimDots()
    {
        float Sx = -direction.x * force;
        float Sy = -direction.y * force;

        for (int i = 0; i < dotArray.Length; i++)
        {
            float t = i * 0.01f;

            float dx = Sx * t;
            float dy = Sy * t - (0.5f * -Physics.gravity.y * t * t);

            dotArray[i].transform.position = new Vector3(transform.position.x + dx, 
                transform.position.y + dy, transform.position.z);
            dotArray[i].SetActive(true);
        }

    } 
}
