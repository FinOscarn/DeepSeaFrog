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
            Debug.Log("�ټ��� GameManager�� ����ǰ��ֽ��ϴ� Ȯ���ϼ���");
        }
    }
}
