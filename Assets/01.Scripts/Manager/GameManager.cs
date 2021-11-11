using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Player player;

    public bool isGameOver = false;

    public int score = 0;

    //게임 시작할때 
    public Action startGame = () => { };
    //플레이어의 다이빙 모션이 끝났을 때
    public Action playerD2ve = () => { };
    //게임오버됐을떄
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

    public void UpdateScore(int score)
    {
        this.score = score;
    }
}
