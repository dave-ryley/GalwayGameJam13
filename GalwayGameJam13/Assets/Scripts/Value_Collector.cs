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
        String score = ("Total score accumulated: " + coinsCollected);
        return String;
    }

    void Update()
    {
        getTotalScore();
    }
}
