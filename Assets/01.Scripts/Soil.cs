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
        //���� �÷��̾ ��Ҵٸ�
        if(collision.CompareTag(PLAYER_TAG))
        {
            //���ӿ����� ȣ�����ش�
            GameManager.instance.gameover();
        }
    }
}
