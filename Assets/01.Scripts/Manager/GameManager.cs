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

    //���� �����Ҷ� 
    public Action startGame = () => { };
    //�÷��̾��� ���̺� ����� ������ ��
    public Action playerD2ve = () => { };
    //���ӿ���������
    public Action gameOver = () => { };

    //�Ͻ�����
    public Action<bool> pause = pause => { };

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

    public void UpdateScore(int score)
    {
        this.score = score;
    }
}
