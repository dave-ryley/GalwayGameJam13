using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Value_Collector : MonoBehaviour
{
    float coinsCollected = 0f;

    public void ReceieveCoins(float amount)
    {
        coinsCollected += amount;
    }

    public String getTotalScore()
    {
        Debug.Log("Total score accumulated: " + coinsCollected);
    }

    void Update()
    {
        getTotalScore();
    }
}
