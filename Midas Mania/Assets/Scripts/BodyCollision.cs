using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyCollision : MonoBehaviour
{
    public Image life1;
    public Image life2;
    public Image life3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.PlayerLostLife();
            int remainingLives = GameManager.Instance.playerLives;
            if (remainingLives == 2)
            {
                life3.enabled = false;
            }
            else if (remainingLives == 1)
            {
                life2.enabled = false;
            }
            else
            {
                life1.enabled = false;
            }

        }
    }
}
