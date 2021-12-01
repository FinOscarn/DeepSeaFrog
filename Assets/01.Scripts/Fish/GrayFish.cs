using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayFish : Fish
{
    public GrayFish()
    {
        type = FishType.Gray;
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
