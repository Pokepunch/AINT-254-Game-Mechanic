using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ItemCounterController : MonoBehaviour {

    Text counter;
    public int itemCount;

	void Start ()
    {
        itemCount = 0;
        counter = gameObject.GetComponent<Text>();
        CollectableItemScript.ItemCollected += OnItemCollected;	
	}

    private void OnDestroy()
    {
        CollectableItemScript.ItemCollected -= OnItemCollected;
    }

    public void OnItemCollected(int amount)
    {
        itemCount += amount;
        counter.text = itemCount.ToString();
    }
}
