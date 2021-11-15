using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Player player;
    public FoodManager foodManager;

    public int highScore = 0;
    public int score = 0;

    //���� �����Ҷ�
    public Action startGame = () => { };
    //�÷��̾��� ���̺� ����� ������ ��
    public Action playerD2ve = () => { };
    //���ӿ���������
    public Action gameover = () => { };
    //������ �ٽý��۵�����
    public Action reset = () => { };

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

        foodManager = GetComponent<FoodManager>();
    }

    private void Start()
    {
        startGame += () =>
        {
            highScore = LoadHighScore();
        };

        gameover += () =>
        {
            SaveHighScore(highScore);
        };

        reset += () =>
        {
            score = 0;
        };
    }

    private void Update()
    {
        if (score > highScore) highScore = score;
    }

    public void UpdateScore(int score)
    {
        this.score = score;
    }

    public void SaveHighScore(int highScore)
    {
        PlayerPrefs.SetInt("HighScore", highScore);
    }

    public int LoadHighScore()
    {
        if(!PlayerPrefs.HasKey("HighScore"))
        {
            SaveHighScore(0);
        }

        int highScore = PlayerPrefs.GetInt("HighScore");

        return highScore;
    }
}
