﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Script : MonoBehaviour
{
    public float curr_health = 10f;
    public float max_health = 10f;
    public bool alive = true;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        curr_health = max_health;
    }

    public void TakeDamage(float Damage_amount)
    {
        curr_health -= Damage_amount;
        if(!alive)
        {
            return;
        }

        if(curr_health <= 0)
        {
            alive = false;
            animator.SetTrigger("Die");
        }
    }
}
