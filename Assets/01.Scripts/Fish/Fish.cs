using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FishType
{
    Green = 0,
    Orange = 1,
    Purple = 2,
    Gray = 3,
    Red = 4,
    Spike = 5,
    Bone = 6,
    StrongerGreen = 7
}

public abstract class Fish : MonoBehaviour
{
    public float moveSpeed = 2; //좌우로 움직이는 속도
    public float downSpeed = 3; //아래로 내려가는 속도

    public bool isLeftMove = false; //왼쪽으로 움직이는지

    public FishType type; //물고기 종류

    [SerializeField]
    protected int maxEatCnt = 1; //최대로 먹을 수 있는 먹이의 갯수
    [SerializeField]
    protected int eatCnt = 0; //현재 먹은 먹이의 갯수

    private const string PLAYER_TAG = "PLAYER"; //플레이어의 태그
    private const string FOOD_TAG = "FOOD"; //음식의 태그

    protected const float MAX_X = 3.6f; //넘어가면 꺼줄 X좌표
    protected const float MIN_X = -3.6f; //넘어가면 꺼줄 X좌표

    protected Player player; //플레이어

    protected SpriteRenderer sr; //스프라이트 렌더러

    public bool isPaused = false;

    private void Awake()
    {
        //스프라이트 랜더러를 가져와준다
        sr = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        //게임메니저에있는 플레이어를 가져와준다
        player = GameManager.instance.player;

        //일시정지 상태일 때 실행해줄 함수를 등록해준다
        GameManager.instance.pause += pause =>
        {
            //일시정지상태를 물고기의 일시정지상태로 바꿔준다
            isPaused = pause;
        };
    }

    protected virtual void Update()
    {
        //만약 일시정지상태라면 리턴
        if (isPaused) return;

        if (isLeftMove)
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            transform.Translate(Vector2.down * downSpeed * Time.deltaTime);

            if (transform.position.x <= MIN_X)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            transform.Translate(Vector2.down * downSpeed * Time.deltaTime);

            if (transform.position.x >= MAX_X)
            {
                gameObject.SetActive(false);
            }
        }
    }

    protected virtual void OnEnable()
    {
        eatCnt = 0;
    }

    public void FlipSprite(bool isLeftMove)
    {
        sr.flipX = isLeftMove;
    }

    /// <summary>
    /// 물고기의 위치값을 초기화합니다
    /// </summary>
    /// <param name="position">초기 위치값</param>
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if(eatCnt >= maxEatCnt)
        {
            
            return;
        }
        else
        {
            if (col.CompareTag(PLAYER_TAG))
            {
                OnPlayerTirgger();
            }
            else if (col.CompareTag(FOOD_TAG))
            {
                Food food = col.gameObject.GetComponent<Food>();
                OnFoodTrigger(food);
            }
        }
    }

    /// <summary>
    /// 물고기에 먹이가 충돌하면 호출되는 함수입니다
    /// </summary>
    /// <param name="food">충돌한 먹이</param>
    protected virtual void OnFoodTrigger(Food food)
    {
        eatCnt++;
    }

    /// <summary>
    /// 물고기에 플레이어가 충돌하면 호출되는 함수입니다
    /// </summary>
    protected virtual void OnPlayerTirgger()
    {

    }
}
