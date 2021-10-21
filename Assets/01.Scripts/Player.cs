using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isCling = false; //먹이에 붙어있는지
    public bool canMove = true; //좌우로 이동할 수 있는지

    public float upSpeed = 1f; //위로 이동하는 속도
    public float downSpeed = 1f; //아래로 떨어지는 속도
    public float moveSpeed = 1f; //좌우로 이동하는 속도

    public Food food = null; //현재 붙어있는 먹이
    public Action<Food> onCling = food => { }; //먹이에 붙었을 때

    private MoveDir moveDir;

    private void Start()
    {
        InputManager.instance.onClick += () =>
        {
            moveDir = InputManager.GetDir();

            if(isCling)
            {
                isCling = false;
                canMove = true;

                if(food != null)
                {
                    food.isCling = false;
                    food = null;
                }
            }
        };
    }

    private void Update()
    {
        if(canMove)
        {
            if (moveDir == MoveDir.Left)
            {
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            }
        }

        if(!isCling)
        {
            transform.Translate(Vector2.up * upSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.down * downSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        print("닿음");

        Food food = col.gameObject.GetComponent<Food>();

        if (food == null || this.food != null) return;

        isCling = true;
        food.isCling = true;
        canMove = false;

        this.food = food;
        this.downSpeed = food.clingSpeed;
    }
}
