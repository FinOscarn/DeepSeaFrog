using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeFIsh : Fish
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        this.downSpeed = player.virSpeed;
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
            GameManager.instance.pause(true);
            //�ӽ÷� ��� �ɾ������ ���߿� ���ӿ����� �ٲ���
        }

        food.Disable();
        this.gameObject.SetActive(false);

        base.OnFoodTrigger(food);
    }

    protected override void OnPlayerTirgger()
    {
        GameManager.instance.pause(true);
        //�ӽ÷� ��� �ɾ������ ���߿� ���ӿ����� �ٲ���
    }
}
