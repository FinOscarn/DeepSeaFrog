using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    public static FishManager instance; //싱글톤 패턴을 위한 인스턴스

    public BlueFish bluefishPrefab; //파도고기의 프리팹

    public Soil soilPrefab; //뼈고기의 분비물 프리펩
    public Mark markPrefab; //파도고기가 나올때 나타나는 마크 프리팹

    public const float maxY = 3f; //물고기가 생성되는 최대 Y좌표
    public const float minY = -3f; //물고기가 생성되는 최소 Y좌표

    public const float maxTime = 3f; //물고기가 생성되는 최대 시간
    public const float minTime = 1f; //물고기가 생성되는 최소 시간

    public readonly float leftSpawnX = -3.4f; //물고기가 왼쪽에서 스폰될 때의 좌표
    public readonly float rightSpawnX = 3.4f; //물고기가 오른쪽에서 스폰될 때의 좌표

    private bool isGameOver = false;

    private Transform player; //플레이어의 트렌스폼

    private Coroutine co; //물고기를 생성하는 코루틴

    private void Awake()
    {
        //만약 인스턴스가 없다면
        if(instance == null)
        {
            //자기자신을 인스턴스에 넣는다
            instance = this;
        }

        //풀메니저에 프리팹을 미리 만들어둔다
        PoolManager.CreatePool<BlueFish>(bluefishPrefab.gameObject, transform, 2);

        PoolManager.CreatePool<Soil>(soilPrefab.gameObject, transform, 10);
        PoolManager.CreatePool<Mark>(markPrefab.gameObject, transform, 2);
    }

    private void Start()
    {
        //플레이어의 트랜스폼은 게임메니저에 있는 플레이어의 트랜스폼으로 가져온다
        player = GameManager.instance.player.transform;

        //게임이 시작되었을때(플레이어의 다이빙 모션이 끝났을 때) 실행할 함수를 추가해준다
        GameManager.instance.playerD2ve += () =>
        {
            //물고기를 생성하는 코루틴을 시작해줌과 동시에 변수에 담아둔다
            co = StartCoroutine(CreateFishRoutine());
        };

        //게임이 일시정지되었을 때 실행할 함수를 추가해준다
        GameManager.instance.pause += pause =>
        {
            //만약 일시정지되었다면
            if (pause)
            {
                //물고기가 생성되는 코루틴을 멈취준다
                StopCoroutine(co);
            }
            else
            {   //물고기를 생성하는 코루틴을 시작해줌과 동시에 변수에 담아둔다
                co = StartCoroutine(CreateFishRoutine());
            }
        };

        GameManager.instance.gameover += () =>
        {
            isGameOver = true;
        };

        GameManager.instance.reset += () =>
        {
            isGameOver = false;
            FishPoolManager.instance.DisableAll();

            PoolManager.DisableAll<BlueFish>();

            PoolManager.DisableAll<Soil>();
            PoolManager.DisableAll<Mark>();
        };
    }

    /// <summary>
    /// 파도고기를 부르는 함수입니다
    /// </summary>
    /// <param name="food">목표 먹이</param>
    public void CallBlueFish(Food food)
    {
        //풀매니저에서 파도고기 하나를 가져온다
        BlueFish fish = PoolManager.GetItem<BlueFish>();

        //bool 값을 랜덤을 뽑는다
        bool isLeftMove = (Random.value > 0.5f);

        //파도고기를 초기회해준다
        fish.Init(food, isLeftMove);
        //파도고기의 이동방향을 맞춰준다
        fish.isLeftMove = isLeftMove;
        //파도고기의 스프라이트가 뒤집히는지 여부를 보내준다
        fish.FlipSprite(isLeftMove);
    }

    /// <summary>
    /// BornFish에서 사용하는 분비물을 리턴하는 함수
    /// </summary>
    /// <param name="position">BornFish의 워치값</param>
    /// <returns></returns>
    public Soil GetSoil(Vector3 position)
    {
        //풀매니저에서 분비물을 가져온다
        Soil soil = PoolManager.GetItem<Soil>();
        //분비물의 위치를 받은 위치로 설정해준다
        soil.transform.position = position;

        //분비물을 리턴
        return soil;
    }

    /// <summary>
    /// 파도고기에서 사용하는 Mark를 리턴하는 함수
    /// </summary>
    /// <param name="food">목표 음식</param>
    /// <returns></returns>
    public Mark GetMark(Food food)
    {
        //풀매니저에서 마크를 하나 가져온다
        Mark mark = PoolManager.GetItem<Mark>();
        //마크의 목표 트랜스폼을 받은 Food의 트랜스폼으로 해준다
        mark.targetTrm = food.gameObject.transform;

        //마크를 리턴
        return mark;
    }

    /// <summary>
    /// 계속해서 물고기를 생성해주는 루틴입니다
    /// </summary>
    /// <returns></returns>
    private IEnumerator CreateFishRoutine()
    {
        //만약 게임오버가 아니라면
        while (!isGameOver)
        {
            //랜덤으로 bool값을 뽑는다
            bool randBool = (Random.value > 0.5f);

            //물고기가 좌에서 생성되는지 우에서 생성되는지를 매개변수로 담아 보낸다
            CreateRandomFish(randBool);

            //물고기가 생성되는 딜레이를 랜덤으로 뽑아준다
            float delay = Random.Range(minTime, maxTime);
            //랜덤으로 뽑은 시간만큼 대기했다가 다시 실행한다
            yield return new WaitForSeconds(delay);
        }
    }

    /// <summary>
    /// 랜덤으로 물고기를 생성하는 함수입니다
    /// </summary>
    private void CreateRandomFish(bool isLeftMove)
    {
        //FishType의 첫번째부터 마지막까지 하나를 랜덤으로 뽑는다
        FishType type = (FishType)Random.Range((int)FishType.Green, (int)FishType.StrongerGreen);

        //나중에 풀링으로 바꾸자
        //FishType에 맞는 물고기를 하나 생성한다
        Fish fish = FishPoolManager.instance.GetFish(type);

        //물고기가 생성될 좌표
        float spawnX;

        //왼쪽으로 움직인다면
        if (isLeftMove)
        {   
            //스폰좌표를 오른쪽으로 해준다
            spawnX = rightSpawnX;
        }
        else
        {
            //스폰좌표를 왼쪽으로 해준다
            spawnX = leftSpawnX;
        }

        //왼쪽으로 움직이는지 여부를 바꿔준다
        fish.isLeftMove = isLeftMove;
        //스프라이트가 뒤집히는지 여부를 보내준다
        fish.FlipSprite(isLeftMove);

        //만약 복어라면
        if (type == FishType.Spike)
        {
            //복어의 위치는 플레이어의 위치와 같은곳에서 스폰된다
            Vector3 spikePos = new Vector3(spawnX, player.position.y, 1);
            //복어의 위치를 세팅해준다
            fish.SetPosition(spikePos);
        }
        else
        {
            //최대값과 최소값 사이에서 y좌표를 뽑는다
            float y = Random.Range(minY, maxY);
            //플레이어의 포지션에 아까 뽑은 y좌표를 더한 위치값
            Vector3 pos = new Vector3(spawnX, player.position.y + y, 1);
            //물고기의 위치를 세팅해준다
            fish.SetPosition(pos);
        }
    }
}
