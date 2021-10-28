using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public float moveSpeed = 2;
    public float downSpeed = 3;

    [SerializeField]
    protected int maxEatCnt = 1; 
    [SerializeField]
    protected int eatCnt = 0;

    private const string PLAYER_TAG = "PLAYER";
    private const string FOOD_TAG = "FOOD";

    protected const float MAX_X = 3.6f;

    protected Player player;

    public bool isPaused = false;

    protected virtual void Start()
    {
        player = GameManager.instance.player;

        GameManager.instance.pause += pause =>
        {
            isPaused = pause;
        };
    }

    protected virtual void Update()
    {
        if (isPaused) return;

        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        transform.Translate(Vector2.down * downSpeed * Time.deltaTime);

        if(transform.position.x >= MAX_X)
        {
            gameObject.SetActive(false);
        }
    }

    protected virtual void OnEnable()
    {
        eatCnt = 0;
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
