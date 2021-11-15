using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFish : Fish
{
    private Mark mark;

    private float moveDelay = 0.5f;
    private bool canMove = false;

    private Food target;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (!canMove) return;
        downSpeed = target.moveSpeed;
        base.Update();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    private void OnDisable()
    {
        canMove = false;
    }

    /// <summary>
    /// �i�ư� ���̿� �ʱ���ġ�� �������ִ� �Լ��Դϴ�
    /// </summary>
    /// <param name="target">�i�ư� ����</param>
    /// <param name="pos">������ ��ġ</param>
    public void Init(Food target, bool isLeftMove)
    {
        StartCoroutine(Setting(target, isLeftMove));
    }

    /// <summary>
    /// �i�ư� ���̿� �ʱ���ġ�� �������ִ� ��ƾ�Դϴ�
    /// </summary>
    /// <param name="target">�i�ư� ����</param>
    /// <param name="pos">������ ��ġ</param>
    private IEnumerator Setting(Food target, bool isLeftMove)
    {
        this.target = target;
        mark = FishManager.instance.GetMark(target);

        yield return new WaitForSeconds(moveDelay);

        Vector3 pos;

        if (isLeftMove)
        {
            pos = new Vector3(FishManager.instance.rightSpawnX, target.transform.position.y, 1);
        }
        else
        {
            pos = new Vector3(FishManager.instance.leftSpawnX, target.transform.position.y, 1);
        }

        transform.position = pos;
        canMove = true;
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);
    }

    protected override void OnFoodTrigger(Food food)
    {
        if (food.IsPlayerFood())
        {
            GameManager.instance.gameover();
        }

        //���� ��ǥ�� �ϰ� �ִ� �����϶��� �浹üũ�� ���ش�
        if(food == this.target)
        {
            food.Disable();
            mark.Disable();

            base.OnFoodTrigger(food);
        }
    }
}
