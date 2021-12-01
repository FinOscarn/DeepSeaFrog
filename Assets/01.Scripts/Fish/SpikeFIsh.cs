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
        //�÷��̾�� �پ��ִ� ���̶��
        if (food.IsPlayerFood())
        {
            //���ӿ����� ȣ�����ش�
            GameManager.instance.gameover();
        }

        //���̸� ��Ȱ��ȭ��Ų��
        food.Disable();
        //��Ȱ��ȭ��Ų��
        this.gameObject.SetActive(false);

        //�ѹ� �� üũ���ش�
        base.OnFoodTrigger(food);
    }

    protected override void OnPlayerTirgger()
    {
        //���ӿ����� ȣ�����ش�
        GameManager.instance.gameover();
    }
}
