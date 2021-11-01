using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BornFish : Fish
{
    public Soil soil;

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
        CreateSoil();

        base.OnFoodTrigger(food);
    }

    private void CreateSoil()
    {
        //���߿� FishManager�� �к��� Ǯ���ϴ� �Լ��� ���� ������ ����
        Instantiate(soil, transform.position, Quaternion.identity);
    }
}