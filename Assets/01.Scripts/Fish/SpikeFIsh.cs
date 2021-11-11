using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeFIsh : Fish
{
    public SpikeFIsh()
    {
        type = FishType.Spike;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        this.downSpeed = player.virSpeed * 0.3f;
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
            //임시로 퍼즈를 걸어놨지만 나중엔 게임오버로 바꾸자
        }

        food.Disable();
        this.gameObject.SetActive(false);

        base.OnFoodTrigger(food);
    }

    protected override void OnPlayerTirgger()
    {
        GameManager.instance.pause(true);
        //임시로 퍼즈를 걸어놨지만 나중엔 게임오버로 바꾸자
    }
}
