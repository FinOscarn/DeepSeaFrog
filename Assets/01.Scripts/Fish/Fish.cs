using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//물고기 종류 enum
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

        //만약 왼쪽으로 향한다면
        if (isLeftMove)
        {
            //위치를 계속 왼쪽으로 이동시킨다
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            //위치를 계속 아래로 이동시킨다
            transform.Translate(Vector2.down * downSpeed * Time.deltaTime);

            //만약 최소 X값보다 작다면
            if (transform.position.x <= MIN_X)
            {
                //비활성화시킨다
                gameObject.SetActive(false);
            }
        }
        else
        {
            //위치를 계속 오른쪽으로 이동시킨다
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            //위치를 계속 아래로 이동시킨다
            transform.Translate(Vector2.down * downSpeed * Time.deltaTime);

            //만약 최대 X값보다 크다면
            if (transform.position.x >= MAX_X)
            {
                //비활성화시킨다
                gameObject.SetActive(false);
            }
        }
    }

    protected virtual void OnEnable()
    {
        //먹은 갯수를 초기화해준다
        eatCnt = 0;
    }

    public void FlipSprite(bool isLeftMove)
    {
        //어디로 이동할지에 따라서 스프라이트를 반전시켜준다
        sr.flipX = isLeftMove;
    }

    /// <summary>
    /// 물고기의 위치값을 초기화합니다
    /// </summary>
    /// <param name="position">초기 위치값</param>
    public void SetPosition(Vector3 position)
    {
        //위치를 세팅해준다
        transform.position = position;
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        //만약 최대로 먹을 수 있는 양보다 많이 먹으려 한다면
        if(eatCnt >= maxEatCnt)
        {
            //리턴해준다
            return;
        }
        else
        {
            //만약 플레이어라면
            if (col.CompareTag(PLAYER_TAG))
            {
                //플레이어가 닿았다고 알려준다
                OnPlayerTirgger();
            }
            //만약 먹이라면
            else if (col.CompareTag(FOOD_TAG))
            {
                //닿은 먹이가 뭔지 가져온다
                Food food = col.gameObject.GetComponent<Food>();
                //먹이가 닿았다고 알려준다
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
        //먹은 갯수를 하나 늘린다
        eatCnt++;
    }

    /// <summary>
    /// 물고기에 플레이어가 충돌하면 호출되는 함수입니다
    /// </summary>
    protected virtual void OnPlayerTirgger()
    {

    }
}
