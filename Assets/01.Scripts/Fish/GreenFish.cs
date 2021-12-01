using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenFish : Fish
{
    public GreenFish()
    {
        type = FishType.Green;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);
    }

    protected override void OnFoodTrigger(Food food)
    {
        //�÷��̾�� �پ��ִ� ���̶��
        if (food.IsPlayerFood())
        {
            //���ӿ����� ȣ�����ش�
            GameManager.instance.gameover();
        }

        //���̸� ��Ȱ��ȭ��Ų��
        food.Disable();
    }
}
