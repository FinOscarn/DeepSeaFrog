using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFish : Fish
{
    //목표 먹이에 표시될 표식
    private Mark mark;

    //표식이 뜨고 움직이기 시작하는 딜레이
    private float moveDelay = 0.5f;
    //움직일 수 있는지 여부
    private bool canMove = false;

    //목표 먹이
    private Food target;

    protected override void Start()
    {
        //Fish 클래스의 Start를 그대로 사용
        base.Start();
    }

    protected override void Update()
    {
        //움직일수없다면 리턴
        if (!canMove) return;

        //목표먹이와 내려가는 속도를 맞춰준다
        downSpeed = target.moveSpeed;
        //부모의 업데이트를 그대로 사용
        base.Update();
    }

    protected override void OnEnable()
    {
        //부모의 OnEnable을 그대로 사용
        base.OnEnable();
    }

    private void OnDisable()
    {
        //움직일 수 없는 상태로 만들어준다
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
        //목표먹이를 설정해준다
        this.target = target;
        //FishManager에서 표식 하나를 가져온다
        mark = FishManager.instance.GetMark(target);

        //딜레이만큼 기다려준다
        yield return new WaitForSeconds(moveDelay);

        Vector3 pos;

        //왼쪽으로 움직일 예정이라면
        if (isLeftMove)
        {
            //오른쪽 끝에 스폰해준다
            pos = new Vector3(FishManager.instance.rightSpawnX, target.transform.position.y, 1);
        }
        //반대라면
        else
        {
            //왼쪽 끝에 스폰해준다
            pos = new Vector3(FishManager.instance.leftSpawnX, target.transform.position.y, 1);
        }

        //위치를 스폰될 위치로 세팅해준다
        transform.position = pos;
        //움직일 수 있는 상태로 바꿔준다
        canMove = true;
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        //부모의 OnTriggerEnter2D를 그대로 써준다
        base.OnTriggerEnter2D(col);
    }

    protected override void OnFoodTrigger(Food food)
    {
        //만약 플레이어가 붙어있는 먹이라면
        if (food.IsPlayerFood())
        {
            //게임오버를 호출해준다
            GameManager.instance.gameover();
        }

        //내가 목표로 하고 있는 먹이일때만 충돌체크를 해준다
        if(food == this.target)
        {
            //먹이를 비활성화시킨다
            food.Disable();
            //표식을 비활성화시킨다
            mark.Disable();

            //다시한번 체크해준다
            base.OnFoodTrigger(food);
        }
    }
}
