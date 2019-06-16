﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCoinsToPlayer : MonoBehaviour
{
    public int coinValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().receieveCoins(coinValue);
            Destroy(gameObject);
        }
    }
}
