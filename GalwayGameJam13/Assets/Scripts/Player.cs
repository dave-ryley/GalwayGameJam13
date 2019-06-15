using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int score = 0;
    
    public void Setup(string key)
    {
        Transform textTransform = transform.GetChild(0);
        TextMesh text = textTransform.GetComponent<TextMesh>();
        text.text = key.ToUpper();
    }
}
