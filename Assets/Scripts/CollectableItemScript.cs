using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItemScript : MonoBehaviour {

    public delegate void OnCollected(int amount);
    public static event OnCollected ItemCollected;

    public int itemValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ItemCollected != null)
            {
                ItemCollected(itemValue);
                gameObject.SetActive(false);
            }
        }
    }
}
