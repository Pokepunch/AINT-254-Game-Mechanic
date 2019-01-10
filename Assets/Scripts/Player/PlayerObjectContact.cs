using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectContact : MonoBehaviour {

    public int damage = 1;
    public string[] damageTag;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        foreach (string tag in damageTag)
        {
            if (hit.gameObject.CompareTag(tag))
            {
                if (tag == "Enemy")
                {
                    if (gameObject.GetComponent<PlayerController2D>().IsDashing)
                    {
                        SendDamage(hit.gameObject);
                    }
                    else if (gameObject.GetComponent<PlayerController2D>().moveDirection.y < 0 && gameObject.transform.position.y > hit.transform.position.y + hit.transform.lossyScale.y)
                    {
                        SendDamage(hit.gameObject);
                        gameObject.GetComponent<PlayerController2D>().moveDirection.y *= -1.2f;
                    }
                    else
                    {
                        SendDamage(gameObject);
                    }
                }
                else if (tag == "Breakable")
                {
                    if (gameObject.GetComponent<PlayerController2D>().IsDashing)
                    {
                        SendDamage(hit.gameObject);
                    }
                }
                else
                {
                    SendDamage(gameObject);
                }
            }
        }
    }

    public void SendDamage(GameObject hit)
    {
        hit.SendMessage("TakeDamage", damage);
    }
}
