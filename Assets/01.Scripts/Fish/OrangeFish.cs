using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeFish : Fish
{
    public OrangeFish()
    {
        type = FishType.Orange;
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
        if (food.IsPlayerFood())
        {
            GameManager.instance.gameOver();
            //�ӽ÷� ��� �ɾ������ ���߿� ���ӿ����� �ٲ���
        }

        food.Disable();

        base.OnFoodTrigger(food);
    }
}
