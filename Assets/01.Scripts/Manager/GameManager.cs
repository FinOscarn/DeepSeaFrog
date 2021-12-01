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
        //�̱��� ����
        if(!instance)
        {
            instance = this;
        }
        else
        {
            Debug.Log("�ټ��� GameManager�� ����ǰ��ֽ��ϴ� Ȯ���ϼ���");
        }

        //FoodManager�� ��Ƶд�
        foodManager = GetComponent<FoodManager>();
    }

    private void Start()
    {
        //������ �ػ󵵸� �������ش�
        Screen.SetResolution(1080, 1920, true);

        //������ ���۵Ǿ��� �� ������ �Լ��� �߰��Ѵ�
        startGame += () =>
        {
            //�ְ������� �ε��Ѵ�
            highScore = LoadHighScore();
        };

        //���ӿ����Ǿ��� �� ������ �Լ��� �߰��Ѵ�
        gameover += () =>
        {
            //�ְ������� �������ش�
            SaveHighScore(highScore);
        };

        //������ ���µǾ��� �� ������ �Լ��� �߰��Ѵ�
        reset += () =>
        {
            //������ �ʱ�ȭ���ش�
            score = 0;
        };
    }

    private void Update()
    {
        //���� ������ �ְ��������� ���ٸ� �ְ������� ������ �ٲ��ش�
        if (score > highScore) highScore = score;
    }

    /// <summary>
    /// ������ ������Ʈ���ִ� �Լ��Դϴ�
    /// </summary>
    /// <param name="score">����</param>
    public void UpdateScore(int score)
    {
        //������ ������Ʈ���ش�
        this.score = score;
    }

    /// <summary>
    /// �ְ������� �������ִ� �Լ��Դϴ�
    /// </summary>
    /// <param name="highScore">�ְ�����</param>
    public void SaveHighScore(int highScore)
    {
        //PlayerPrefs�� �������ش�
        PlayerPrefs.SetInt("HighScore", highScore);
    }

    /// <summary>
    /// �ְ������� �ҷ����� �Լ��Դϴ�
    /// </summary>
    /// <returns></returns>
    public int LoadHighScore()
    {
        //���� �ְ������� ����Ǿ����� �ʴٸ�
        if(!PlayerPrefs.HasKey("HighScore"))
        {
            //�ְ������� 0���� �����س��´�
            SaveHighScore(0);
        }

        //�ְ������� �����´�
        int highScore = PlayerPrefs.GetInt("HighScore");
        //������ �ְ������� ����
        return highScore;
    }
}
