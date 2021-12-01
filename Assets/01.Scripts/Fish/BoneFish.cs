using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneFish : Fish
{
    //분비물오브젝트
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
        //만약 플레이어에 닿았다면
        if (food.IsPlayerFood())
        {
            GameManager.instance.gameover();
            //임시로 퍼즈를 걸어놨지만 나중엔 게임오버로 바꾸자
        }

        //닿은먹이를 비활성화시킨다
        food.Disable();
        //FishManager에서 Pool에있는 Soil을 가져온다
        FishManager.instance.GetSoil(transform.position);

        //한번 더 체크해준다
        base.OnFoodTrigger(food);
    }
}
