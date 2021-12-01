using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : Food
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //만약 플레이어가 닿았다면
        if(collision.CompareTag(PLAYER_TAG))
        {
            //게임오버를 호출해준다
            GameManager.instance.gameover();
        }
    }
}
