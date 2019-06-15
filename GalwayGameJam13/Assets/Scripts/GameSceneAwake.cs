using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneAwake : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GGJGameManager.SetState("joinGame");
    }
}
