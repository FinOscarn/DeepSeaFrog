using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isCling = false; //���̿� �پ��ִ���
    public bool canMove = true; //�¿�� �̵��� �� �ִ���

    public float upSpeed = 1f; //���� �̵��ϴ� �ӵ�
    public float downSpeed = 1f; //�Ʒ��� �������� �ӵ�
    public float moveSpeed = 1f; //�¿�� �̵��ϴ� �ӵ�

    public Food food = null; //���� �پ��ִ� ����
    public Action<Food> onCling = food => { }; //���̿� �پ��� ��

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
        print("����");

        Food food = col.gameObject.GetComponent<Food>();

        if (food == null || this.food != null) return;

        isCling = true;
        food.isCling = true;
        canMove = false;

        this.food = food;
        this.downSpeed = food.clingSpeed;
    }
}
