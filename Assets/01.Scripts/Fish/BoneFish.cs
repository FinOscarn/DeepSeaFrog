using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneFish : Fish
{
    public Soil soil;

    public BoneFish()
    {
        type = FishType.Bone;
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
            GameManager.instance.pause(true);
            //�ӽ÷� ��� �ɾ������ ���߿� ���ӿ����� �ٲ���
        }

        food.Disable();
        //FishManager���� Pool���ִ� Soil�� �����´�
        FishManager.instance.GetSoil(transform.position);

        base.OnFoodTrigger(food);
    }
}
