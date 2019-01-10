using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public float cameraHeightDiff;

    // The boundries for the camera. If the player leaves the camera's view they'll die.
    // x = the furthest to the left the camera will go.
    // y = the furthest to the right the camera will go. 
    public Vector2 cameraBoundsX;
    // x = the furthest the camera will go down.
    // y = the furthest the camera will go up. 
    public Vector2 cameraBoundsY;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 cameraNewPos = transform.position;
        if (playerPos.x > cameraBoundsX.x && playerPos.x < cameraBoundsX.y)
        {
            cameraNewPos.x = playerPos.x;
        }
        if (playerPos.y > cameraBoundsY.x && playerPos.y < cameraBoundsY.y)
        {
            cameraNewPos.y = playerPos.y + cameraHeightDiff;
        }

        // If the player goes below the camera, kill them.
        Vector3 viewPos = Camera.main.WorldToViewportPoint(playerPos);
        if (viewPos.y < 0)
        {
            player.SendMessage("TakeDamage", 100);
        }
        transform.position = cameraNewPos;
    }

    // Doing this again in LateUpdate reduces small stutter on player object
    void LateUpdate()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 cameraNewPos = transform.position;
        if (playerPos.x > cameraBoundsX.x && playerPos.x < cameraBoundsX.y)
        {
            cameraNewPos.x = playerPos.x;
        }
        if (playerPos.y > cameraBoundsY.x && playerPos.y < cameraBoundsY.y)
        {
            cameraNewPos.y = playerPos.y + cameraHeightDiff;
        }
        transform.position = cameraNewPos;
    }
}
