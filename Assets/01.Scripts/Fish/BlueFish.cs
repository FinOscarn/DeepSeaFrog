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
    /// 쫒아갈 먹이와 초기위치를 세팅해주는 함수입니다
    /// </summary>
    /// <param name="target">쫒아갈 먹이</param>
    /// <param name="pos">스폰될 위치</param>
    public void Init(Food target, bool isLeftMove)
    {
        StartCoroutine(Setting(target, isLeftMove));
    }

    /// <summary>
    /// 쫒아갈 먹이와 초기위치를 세팅해주는 루틴입니다
    /// </summary>
    /// <param name="target">쫒아갈 먹이</param>
    /// <param name="pos">스폰될 위치</param>
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

        //내가 목표로 하고 있는 먹이일때만 충돌체크를 해준다
        if(food == this.target)
        {
            food.Disable();
            mark.Disable();

            base.OnFoodTrigger(food);
        }
    }
}
