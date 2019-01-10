using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpNodeScript : MonoBehaviour
{
    public bool isFirstNode;
    public bool isLastNode;
    public Vector2 travelDirection;

    private bool movingPlayerToCenter = false;
    private GameObject nextNode;
    private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            if (player.GetComponent<PlayerController2D>().IsDashing)
            {
                if (isFirstNode)
                {
                    PlayerController2D.controlLocked = true;
                    PlayerController2D.isDashInfinite = true;
                    player.GetComponent<PlayerController2D>().dashTime = PlayerController2D.dashTimeBack;
                }
                if (player.transform.position != transform.position && PlayerController2D.isDashInfinite)
                {
                    player.GetComponent<PlayerController2D>().movement = Vector2.zero;
                    movingPlayerToCenter = true;
                }
                else if(PlayerController2D.isDashInfinite)
                {
                    StartWarp();
                }
            }
        }
    }

    private void Update()
    {
        if (movingPlayerToCenter)
        {
            if (Vector3.Distance(transform.position, player.transform.position) > 0.5)
            {
                float x = 0, y = 0;
                if (player.transform.position.x > transform.position.x)
                {
                    x = -0.07f;
                }
                else
                {
                    x = 0.07f;
                }
                if (player.transform.position.y > transform.position.y)
                {
                    y = -0.07f;
                }
                else
                {
                    y = 0.07f;
                }
                player.transform.position = new Vector3(player.transform.position.x + x, player.transform.position.y + y, player.transform.position.z);
            }
            else
            {
                movingPlayerToCenter = false;
                player.transform.position = transform.position;
                StartWarp();
            }
        }
    }

    private void StartWarp()
    {
        if (isLastNode)
        {
            PlayerController2D.controlLocked = false;
            PlayerController2D.isDashInfinite = false;
            player.GetComponent<PlayerController2D>().dashTime = PlayerController2D.dashTimeBack;
        }
        player.GetComponent<PlayerController2D>().movement = travelDirection;
    }

}
