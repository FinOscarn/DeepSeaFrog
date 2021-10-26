using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Player player;

    public bool isGameOver = false;
    public Action startGame = () => { };

    private void Awake()
    {
        if(!instance)
        {
            instance = this;
        }
        else
        {
            Debug.Log("다수의 GameManager가 실행되고있습니다 확인하세요");
        }
    }
}
