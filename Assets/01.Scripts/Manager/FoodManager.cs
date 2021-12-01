using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    //먹이 프리팹을 저장해놓는다
    public GameObject foodPrefab;

    //먹이가 생성되는 x값 범위를 상수로 지정해놓는다
    public const float maxX = 2.313f;
    public const float minX = -2.313f;

    //플레이어보다 얼마나 위에서 스폰되는지 상수로 지정해놓는다
    public const float plusY = 10f;
    //생성주기를 상수로 지정해놓는다
    public const float createDelay = 1f;

    //게임오버 여부
    private bool isGameOver = false;

    //코루틴 대기시간
    private WaitForSeconds ws;
    //플레이어의 위치
    private Transform player;

    //먹이를 생성하는 코루틴
    private Coroutine co;

    private void Awake()
    {
        //풀매니저에 프리팹을 미리 만들어둔다
        PoolManager.CreatePool<Food>(foodPrefab, transform, 10);
        //코루틴 대기시간을 생성주기로 설정해놓는다
        ws = new WaitForSeconds(createDelay);
    }

    private void Start()
    {
        //플레이어의 Transform을 가져온다
        player = GameManager.instance.player.transform;

        //게임이 시작되었을때(플레이어의 다이빙 모션이 끝났을 때) 실행할 함수를 추가해준다
        GameManager.instance.playerD2ve += () =>
        {
            //먹이를 만드는 코루틴을 시작하는 동시에 변수에 담아둔다
            co = StartCoroutine(CreateFood());
        };

        //게임이 일시정지되었을 때 실행할 함수를 등록해준다
        GameManager.instance.pause += pause =>
        {
            //만약 일시정지되었다면
            if (pause)
            {
                if(co != null)
                {
                    //코루틴이 실행중이라면 멈춰준다
                    StopCoroutine(co);
                }
            }
            //아니라면
            else
            {
                //먹이를 만드는 코루틴을 시작하는 동시에 변수에 담아둔다
                co = StartCoroutine(CreateFood());
            }
        };

        //게임오버되었을 때 실행할 함수를 추가해준다
        GameManager.instance.gameover += () =>
        {
            //게임오버를 켜준다
            isGameOver = true;
        };

        //게임이 리셋되었을 때 실행할 함수를 추가해준다
        GameManager.instance.reset += () =>
        {
            //게임오버를 꺼준다
            isGameOver = false;
            //실행중인 코루틴을 멈춰준다
            StopCoroutine(co);
            //모든 먹이오브젝트를 비활성화시킨다
            PoolManager.DisableAll<Food>();
        };
    }

    /// <summary>
    /// 먹이를 하나 가져오는 함수입니다
    /// </summary>
    /// <param name="pos">먹이를 생성할 위치</param>
    /// <returns></returns>
    public Food GetFood(Vector3 pos)
    {
        //풀매니저에서 먹이 하나를 가져온다
        Food food = PoolManager.GetItem<Food>();
        //먹이의 위치를 초기화해준다
        food.Init(pos);

        //가져온 먹이를 리턴
        return food;
    }

    /// <summary>
    /// 먹이를 계속 생성해주는 루틴입니다
    /// </summary>
    /// <returns></returns>
    private IEnumerator CreateFood()
    {
        //게임오버상태가 아니라면
        while (!isGameOver)
        {
            //풀매니저에서 먹이 하나를 가져온다
            Food food = PoolManager.GetItem<Food>();
            //플레이어의 위치를 기반으로 먹이를 생성할 위치를 랜덤으로 결정한다
            Vector3 pos = new Vector3(Random.Range(minX, maxX), player.position.y + plusY, 0);
            //먹이의 위치를 초기화
            food.Init(pos);

            //생성 딜레이만큼 기다려준다
            yield return ws;
        }
    }
}
