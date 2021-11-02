using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isPaused = true; //�Ͻ������Ǿ�����
    
    public bool isCling = false; //���̿� �پ��ִ���
    public bool canMove = true; //�¿�� �̵��� �� �ִ���

    public float upSpeed = 1f; //���� �̵��ϴ� �ӵ�, �ݴ�����̴ϱ� -�� �ٿ�����
    public float downSpeed = 1f; //�Ʒ��� �������� �ӵ�

    public float virSpeed = 1f; //���Ϸ� �̵��ϴ� �ӵ�
    public float horSpeed = 1f; //�¿�� �̵��ϴ� �ӵ�

    public float clingTimer; //�� �ʰ� ���̿� �پ��־�����
    public float maxTime = 3f; //�ִ�� ���̿� �پ����� �� �ִ� �ð�

    public Food food = null; //���� �پ��ִ� ����
    public Action<Food> onCling = food => { }; //���̿� �پ��� ��

    private MoveDir moveDir; //�÷��̾��� �̵�����

    private void Start()
    {
        //������ ���۵Ǿ��� �� ������ �Լ��� ������ش�
        GameManager.instance.startGame += () =>
        {
            //�Ͻ������� ���ش�
            isPaused = false;
        };

        //������ �Ͻ������Ǿ��� �� ������ �Լ��� ������ش�
        GameManager.instance.pause += pause =>
        {
            //�Ͻ����� ���¿� �����ϰ� ����
            isPaused = pause;
        };

        //ȭ���� ������ �� ����� �Լ��� ������ش�
        InputManager.instance.onClick += () =>
        {
            //�̵������� InputManager���� ������ �̵�����
            moveDir = InputManager.GetDir();

            //���� ���̿� �پ��ִ� ���¶��
            if(isCling)
            {
                //���̿��� �������� ���ش�
                DisuniteFood(food);
            }
        };
    }

    private void Update()
    {
        //���� �Ͻ��������¶�� return
        if (isPaused) return;

        //�¿�� ������ �� �ִ� ���¶��
        if(canMove)
        {
            //�̵������� �����̶��
            if (moveDir == MoveDir.Left)
            {
                //horSpeed��ŭ ��� �������� �����δ�
                transform.Translate(Vector2.left * horSpeed * Time.deltaTime);
            }
            else
            {   //horSpeed��ŭ ��� ���������� �����δ�
                transform.Translate(Vector2.right * horSpeed * Time.deltaTime);
            }
        }

        //���� ���̿� �پ��������� ���¶��
        if(!isCling)
        {
            //�����̵��ӵ��� upSpeed�� �����ش�
            virSpeed = upSpeed;
        }
        else
        {
            //�����̵��ӵ��� downSpeed�� �����ش�
            virSpeed = downSpeed;
            //���̿� �پ��ִ� �ð��� ��� �����ش�
            clingTimer += Time.deltaTime;
        }
        // virSpeed ��ŭ ��� �Ʒ��� �����ش�
        transform.Translate(Vector2.down * virSpeed * Time.deltaTime);

        //���� �ִ�� �پ����� �� �ִ� �ð���ŭ �پ��־��ٸ�
        if (clingTimer >= maxTime)
        {
            //FishManager���� �ĵ���⸦ �θ���
            FishManager.instance.CallBlueFish(food);
            //Ÿ�̸Ӵ� �ٽ� 0���� �ٲ��ش�
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
        //�پ��ִ� ���̰� null �̰� food�� ���������� true�� ����
        return this.food == null && food.gameObject.activeSelf;
    }

    /// <summary>
    /// �÷��̾ ���̿� �ٴ� �Լ��Դϴ�
    /// </summary>
    /// <param name="food">���� ����</param>
    public void UniteFood(Food food)
    {
        //�پ��ִ� ���¸� true�� �ٲ��ְ�
        isCling = true;
        //������ �پ��ִ� ���µ� true�� �ٲ��ش�
        food.isCling = true;

        //�¿�δ� ������ �� ���� ���ش�
        canMove = false;

        //player�� food�� ���� food�� �ٲ��ְ�
        this.food = food;
        //downSpeed�� ���̰� �������ִ� �پ������� �������� �ӵ��� �ٲ��ش�
        this.downSpeed = food.clingSpeed;
    }

    /// <summary>
    /// �÷��̾ ���̿��� ������ �������� �Լ��Դϴ�
    /// </summary>
    /// <param name="food">������ ����</param>
    public void DisuniteFood(Food food)
    {
        //�پ��ִ� ���¸� ���ְ�
        isCling = false;
        //���̵� �پ����� ���� ���·� ������ش�
        food.isCling = false;

        //�ٽ� �¿�� ������ �� �ְ� ���ش�
        canMove = true;

        //food�� �ٽ� null�� �ٲ��ش�
        this.food = null;

        //�پ��ִ� �ð��� �ʱ�ȭ���ش�
        clingTimer = 0f;
    }
}
