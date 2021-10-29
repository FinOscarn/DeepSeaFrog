using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isPaused = true;
    
    public bool isCling = false; //먹이에 붙어있는지
    public bool canMove = true; //좌우로 이동할 수 있는지

    public float upSpeed = 1f; //위로 이동하는 속도, 반대방향이니까 -를 붙여주자
    public float downSpeed = 1f; //아래로 떨어지는 속도

    public float virSpeed = 1f; //상하로 이동하는 속도
    public float horSpeed = 1f; //좌우로 이동하는 속도

    public float clingTimer;
    public float maxTime = 3f;

    public Food food = null; //현재 붙어있는 먹이
    public Action<Food> onCling = food => { }; //먹이에 붙었을 때

    private MoveDir moveDir;

    private void Start()
    {
        GameManager.instance.startGame += () =>
        {
            isPaused = false;
        };

        GameManager.instance.pause += pause =>
        {
            isPaused = pause;
        };

        InputManager.instance.onClick += () =>
        {
            moveDir = InputManager.GetDir();

            if(isCling)
            {
                DisuniteFood(food);
            }
        };
    }

    private void Update()
    {
        if (isPaused) return;

        if(canMove)
        {
            if (moveDir == MoveDir.Left)
            {
                transform.Translate(Vector2.left * horSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector2.right * horSpeed * Time.deltaTime);
            }
        }

        if(!isCling)
        {
            virSpeed = upSpeed;
        }
        else
        {
            virSpeed = downSpeed;
            clingTimer += Time.deltaTime;
        }

        transform.Translate(Vector2.down * virSpeed * Time.deltaTime);

        if (clingTimer >= maxTime)
        {
            FishManager.instance.CallBlueFish(food);
            clingTimer = 0;
        }
    }

    /// <summary>
    /// 플레이어가 먹이에 붙을 수 있는 상태인지를 반환합니다
    /// </summary>
    /// <param name="food">먹이</param>
    /// <returns>플레이어가 먹이에 붙을 수 있는 상태</returns>
    public bool CanUnite(Food food)
    {
        return this.food == null && food.gameObject.activeSelf;
    }

    /// <summary>
    /// 플레이어가 먹이에 붙는 함수입니다
    /// </summary>
    /// <param name="food">붙을 먹이</param>
    public void UniteFood(Food food)
    {
        isCling = true;
        food.isCling = true;

        canMove = false;

        this.food = food;
        this.downSpeed = food.clingSpeed;
    }

    /// <summary>
    /// 플레이어가 먹이에서 강제로 떨어지는 함수입니다
    /// </summary>
    /// <param name="food">떨어질 먹이</param>
    public void DisuniteFood(Food food)
    {
        isCling = false;
        food.isCling = false;

        canMove = true;

        this.food = null;

        clingTimer = 0f;
    }
}
