using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCoinsToPlayer : MonoBehaviour
{
    public float coinValue = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Value_Collector>().ReceieveCoins(coinValue);
            Destroy(gameObject);
        }
    }
}
