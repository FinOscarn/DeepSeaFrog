using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGObjectManager : MonoBehaviour
{
    public GameObject bgObjectPrefab; //배경오브젝트 프리팹

    public const float maxX = 2.313f; //최대 X좌표
    public const float minX = -2.313f; //최소 Y 좌표

    public const float minusY = 5f; //플레이어로부터 얼마나 밑에 스폰되는지
    public const float createDelay = 0.5f; //생성 주기

    private WaitForSeconds ws; //메모리 절약을 위한 인스턴스
    private Transform playerTrm; //플레이어의 위치값
    private Player player; //플레이어

    private Coroutine co;

    private void Awake()
    {
        //풀매니저에 배경오브젝트를 미리 생성해놓는다
        PoolManager.CreatePool<BGObject>(bgObjectPrefab, transform, 10);
        //생성주기에 맞춰 Ws를 세팅
        ws = new WaitForSeconds(createDelay);
    }

    private void Start()
    {
        //게임매니저에서 플레이어를 가져온다
        player = GameManager.instance.player;
        //플레이어의 트랜스폽을 따로 저장해놓는다
        playerTrm = player.transform;

        //게임이 시작되었을 때 실행할 함수를 등록해준다
        GameManager.instance.startGame += () =>
        {
            //배경오브젝트를 만드는 코루틴을 실행해주고 변수에 담아둔다
            co = StartCoroutine(CreateBGObject());
        };

        GameManager.instance.pause += pause =>
        {
            //만약 일시정지라면
            if(pause)
            {
                //실행중이던 코루틴을 멈춰준다
                StopCoroutine(co);
            }
            else
            {
                //배경오브젝트를 만드는 코루틴을 실행해주고 변수에 담아둔다
                co = StartCoroutine(CreateBGObject());
            }
        };
    }

    /// <summary>
    /// 배경오브젝트를 계속해서 만들어주는 함수입니다
    /// </summary>
    /// <returns></returns>
    private IEnumerator CreateBGObject()
    {
        //만약 게임오버상태가 아니라면
        while (!GameManager.instance.isGameOver)
        {
            //만약 플레이어가 먹이에 붙어있다면
            if (player.isCling)
            {
                //풀메니저에서 배경오브젝트 하나를 가져온다
                BGObject bGObject = PoolManager.GetItem<BGObject>();
                //X위치값을 랜덤으로 뽑아주고 플레이어의 위치에서 minusY를 빼준 위치값
                Vector3 pos = new Vector3(Random.Range(minX, maxX), playerTrm.position.y - minusY, 0);

                //배경오브젝트를 초기화 해준다
                bGObject.Init(pos);
            }

            //createDelay 후에 코루틴이 다시 실행되게 해준다
            yield return ws;
        }
    }
}
