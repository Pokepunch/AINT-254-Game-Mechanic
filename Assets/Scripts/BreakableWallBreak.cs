using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWallBreak : MonoBehaviour
{
    public GameObject[] Parts;

    public float forceX;
    public float[] forceY;

    private void TakeDamage(int damage)
    {
        int direction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController2D>().IsLeft;
        for (int i = 0; i < Parts.Length; i++)
        {
            Parts[i].SetActive(true);
            Parts[i].GetComponent<Rigidbody>().velocity = new Vector3(forceX * direction, forceY[i]);
        }
        gameObject.SetActive(false);
    }
}
