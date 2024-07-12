using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSinleton<GameManager>
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }



}

