using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleFish : Fish
{
    public PurpleFish()
    {
        type = FishType.Purple;
    }

    protected override void Start()
    {
        base.Start();
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
            //�÷��̾�� ����߸���
            player.DisuniteFood(food);
        }

        //���̸� ��Ȱ��ȭ��Ų��
        food.Disable();

        //�ѹ� �� üũ���ش�
        base.OnFoodTrigger(food);
    }
}
