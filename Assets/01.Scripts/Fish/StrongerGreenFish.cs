using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongerGreenFish : Fish
{
    public StrongerGreenFish()
    {
        type = FishType.StrongerGreen;
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
            //���ӿ����� ȣ�����ش�
            GameManager.instance.gameover();
        }

        //���̸� ��Ȱ��ȭ��Ų��
        food.Disable();

        //�ѹ� �� üũ���ش�
        base.OnFoodTrigger(food);
    }

    protected override void OnPlayerTirgger()
    {
        //���ӿ����� ȣ�����ش�
        GameManager.instance.gameover();
    }
}
