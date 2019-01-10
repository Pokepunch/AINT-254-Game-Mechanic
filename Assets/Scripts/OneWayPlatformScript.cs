using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatformScript : MonoBehaviour {

    public BoxCollider _collider;
    private bool hasCollision = true;
    private Collider player;

    // Only go through Update checks when needed.
    private bool needsUpdate = false;

    // If the player enters the trigger, turn off the collision.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other;
            hasCollision = false;
            needsUpdate = true;
        }
    }

    private void Update()
    {
        if (hasCollision && needsUpdate)
        {
            // If the player has left the trigger they might still be inside the platform as they are jumping etc, 
            // So only enable collision if the player is moving downwards. 
            if (player.GetComponent<PlayerController2D>().moveDirection.y <= 0)
            {
                _collider.enabled = true;
                needsUpdate = false;
            }
        }
        else if (!hasCollision && needsUpdate)
        {
            _collider.enabled = false;
            needsUpdate = false;
        }
    }

    // If the player leavs the trigger, turn on the collision.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasCollision = true;
            needsUpdate = true;
        }
    }
}
