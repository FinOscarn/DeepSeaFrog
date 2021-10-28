using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Player player;

    public bool isGameOver = false;

    //게임 시작할때 
    public Action startGame = () => { };
    public Action gameOver = () => { };

    //일시정지
    public Action<bool> pause = pause => { };

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
