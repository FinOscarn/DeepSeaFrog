using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isPaused = true;
    
    public bool isCling = false; //���̿� �پ��ִ���
    public bool canMove = true; //�¿�� �̵��� �� �ִ���

    public float upSpeed = 1f; //���� �̵��ϴ� �ӵ�, �ݴ�����̴ϱ� -�� �ٿ�����
    public float downSpeed = 1f; //�Ʒ��� �������� �ӵ�

    public float virSpeed = 1f; //���Ϸ� �̵��ϴ� �ӵ�
    public float horSpeed = 1f; //�¿�� �̵��ϴ� �ӵ�

    public float clingTimer;
    public float maxTime = 3f;

    public Food food = null; //���� �پ��ִ� ����
    public Action<Food> onCling = food => { }; //���̿� �پ��� ��

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
    /// �÷��̾ ���̿� ���� �� �ִ� ���������� ��ȯ�մϴ�
    /// </summary>
    /// <param name="food">����</param>
    /// <returns>�÷��̾ ���̿� ���� �� �ִ� ����</returns>
    public bool CanUnite(Food food)
    {
        return this.food == null && food.gameObject.activeSelf;
    }

    /// <summary>
    /// �÷��̾ ���̿� �ٴ� �Լ��Դϴ�
    /// </summary>
    /// <param name="food">���� ����</param>
    public void UniteFood(Food food)
    {
        isCling = true;
        food.isCling = true;

        canMove = false;

        this.food = food;
        this.downSpeed = food.clingSpeed;
    }

    /// <summary>
    /// �÷��̾ ���̿��� ������ �������� �Լ��Դϴ�
    /// </summary>
    /// <param name="food">������ ����</param>
    public void DisuniteFood(Food food)
    {
        isCling = false;
        food.isCling = false;

        canMove = true;

        this.food = null;

        clingTimer = 0f;
    }
}
