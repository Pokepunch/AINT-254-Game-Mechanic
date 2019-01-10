using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    public Image healthFill;

	void Start ()
    {
        PlayerHealthController.PlayerOnHealthChanged += OnHealthChanged;
	}

    private void OnDisable()
    {
        PlayerHealthController.PlayerOnHealthChanged -= OnHealthChanged;
    }

    public void OnHealthChanged(int health)
    {
        healthFill.fillAmount = (float)health / 5;
    }
}
