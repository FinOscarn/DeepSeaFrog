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

    //게임 시작할때
    public Action startGame = () => { };
    //플레이어의 다이빙 모션이 끝났을 때
    public Action playerD2ve = () => { };
    //게임오버됐을떄
    public Action gameover = () => { };
    //게임이 다시시작됐을때
    public Action reset = () => { };

    //일시정지
    public Action<bool> pause = pause => { };

    private void Awake()
    {
        //싱글톤 패턴
        if(!instance)
        {
            instance = this;
        }
        else
        {
            Debug.Log("다수의 GameManager가 실행되고있습니다 확인하세요");
        }

        //FoodManager를 담아둔다
        foodManager = GetComponent<FoodManager>();
    }

    private void Start()
    {
        //게임의 해상도를 고정해준다
        Screen.SetResolution(1080, 1920, true);

        //게임이 시작되었을 때 실행할 함수를 추가한다
        startGame += () =>
        {
            //최고점수를 로드한다
            highScore = LoadHighScore();
        };

        //게임오버되었을 때 실행할 함수를 추가한다
        gameover += () =>
        {
            //최고점수를 저장해준다
            SaveHighScore(highScore);
        };

        //게임이 리셋되었을 떄 실행할 함수를 추가한다
        reset += () =>
        {
            //점수를 초기화해준다
            score = 0;
        };
    }

    private void Update()
    {
        //만약 점수가 최고점수보다 높다면 최고점수를 점수로 바꿔준다
        if (score > highScore) highScore = score;
    }

    /// <summary>
    /// 점수를 업데이트해주는 함수입니다
    /// </summary>
    /// <param name="score">점수</param>
    public void UpdateScore(int score)
    {
        //점수를 업데이트해준다
        this.score = score;
    }

    /// <summary>
    /// 최고점수를 저장해주는 함수입니다
    /// </summary>
    /// <param name="highScore">최고점수</param>
    public void SaveHighScore(int highScore)
    {
        //PlayerPrefs로 저장해준다
        PlayerPrefs.SetInt("HighScore", highScore);
    }

    /// <summary>
    /// 최고점수를 불러오는 함수입니다
    /// </summary>
    /// <returns></returns>
    public int LoadHighScore()
    {
        //만약 최고점수가 저장되어있지 않다면
        if(!PlayerPrefs.HasKey("HighScore"))
        {
            //최고점수를 0으로 저장해놓는다
            SaveHighScore(0);
        }

        //최고점수를 가져온다
        int highScore = PlayerPrefs.GetInt("HighScore");
        //가져온 최고점수룰 리턴
        return highScore;
    }
}
