using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BornFish : Fish
{
    private Soil soil;

    private void Awake()
    {
        soil = GetComponentInChildren<Soil>();
        soil.gameObject.SetActive(false);
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
            //임시로 퍼즈를 걸어놨지만 나중엔 게임오버로 바꾸자
        }

        food.Disable();
        CreateSoil();

        base.OnFoodTrigger(food);
    }

    private void CreateSoil()
    {
        //나중에 FishManager에 분비물을 풀링하는 함수를 만들어서 가져와 쓰자
        soil.gameObject.SetActive(false);
    }
}
