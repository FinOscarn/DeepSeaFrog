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
        //플레이어와 붙어있는 먹이라면
        if (food.IsPlayerFood())
        {
            //플레이어와 떨어뜨린다
            player.DisuniteFood(food);
        }

        //먹이를 비활성화시킨다
        food.Disable();

        //한번 더 체크해준다
        base.OnFoodTrigger(food);
    }
}
