using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {

    public int health = 10;
    public bool canDamage = true;

    int flashCounter;
    float frameTimer;
    bool visible = true;
    public int flashTimes = 4;
    public int flashFrameGap = 12;

    public void TakeDamage(int damage)
    {
        if (flashCounter == 0 && canDamage == true)
        {
            health -= damage;
            flashCounter = flashTimes;
            if (health <= 0)
            {
                //PlayerDead();
                gameObject.SetActive(false);
            }
        }
    }

    public void RestoreHealth(int amount)
    {
        int _health = health;
        _health += amount;
        if (_health > 10)
        {
            _health = 10;
        }
        health = _health;
    }

    private void FixedUpdate()
    {
        if (flashCounter > 0)
        {
            frameTimer++;
            if (frameTimer % flashFrameGap == 0)
            {
                visible = !visible;
                //ToggleVisible(visible);
                if (visible)
                {
                    flashCounter--;
                    if (flashCounter == 0)
                    {
                        frameTimer = 0;
                    }
                }
            }
        }
    }

    private void ToggleVisible(bool visible)
    {

    }
}
