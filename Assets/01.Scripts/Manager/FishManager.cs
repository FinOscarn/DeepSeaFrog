using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    public static FishManager instance;

    [SerializeField]
    private List<Fish> fishList = new List<Fish>();

    public BlueFish bluefishPrefab;

    public Soil soilPrefab;
    public Mark markPrefab;

    public const float maxY = 3f;
    public const float minY = -3f;

    public const float maxTime = 5f;
    public const float minTime = 1f;

    public readonly float leftSpawnX = -3.4f;
    public readonly float rightSpawnX = 3.4f;

    private Transform player;

    private Coroutine co;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        PoolManager.CreatePool<BlueFish>(bluefishPrefab.gameObject, transform, 2);

        PoolManager.CreatePool<Soil>(soilPrefab.gameObject, transform, 10);
        PoolManager.CreatePool<Mark>(markPrefab.gameObject, transform, 2);
    }

    private void Start()
    {
        player = GameManager.instance.player.transform;

        GameManager.instance.startGame += () =>
        {
            co = StartCoroutine(CreateFishRoutine());
        };

        GameManager.instance.pause += pause =>
        {
            if (pause)
            {
                StopCoroutine(co);
            }
            else
            {
                co = StartCoroutine(CreateFishRoutine());
            }
        };
    }

    /// <summary>
    /// �ĵ���⸦ �θ��� �Լ��Դϴ�
    /// </summary>
    /// <param name="food">��ǥ ����</param>
    public void CallBlueFish(Food food)
    {
        BlueFish fish = PoolManager.GetItem<BlueFish>();

        bool isLeftMove = (Random.value > 0.5f);

        fish.Init(food, isLeftMove);
        fish.isLeftMove = isLeftMove;
        fish.FlipSprite(isLeftMove);
    }

    /// <summary>
    /// BornFish���� ����ϴ� �к��� �����ϴ� �Լ�
    /// </summary>
    /// <param name="position">BornFish�� ��ġ��</param>
    /// <returns></returns>
    public Soil GetSoil(Vector3 position)
    {
        Soil soil = PoolManager.GetItem<Soil>();
        soil.transform.position = position;

        return soil;
    }

    public Mark GetMark(Food food)
    {
        Mark mark = PoolManager.GetItem<Mark>();
        mark.targetTrm = food.gameObject.transform;

        return mark;
    }

    /// <summary>
    /// ����ؼ� ����⸦ �������ִ� ��ƾ�Դϴ�
    /// </summary>
    /// <returns></returns>
    private IEnumerator CreateFishRoutine()
    {
        while (!GameManager.instance.isGameOver)
        {
            bool randBool = (Random.value > 0.5f);

            CreateRandomFish(randBool);

            float delay = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(delay);
        }
    }

    /// <summary>
    /// �������� ����⸦ �����ϴ� �Լ��Դϴ�
    /// </summary>
    /// <param name="spawnX"></param>
    private void CreateRandomFish(bool isLeftMove)
    {
        FishType type = (FishType)Random.Range((int)FishType.Green, (int)FishType.StrongerGreen);

        //���߿� Ǯ������ �ٲ���
        Fish fish = Instantiate(fishList[(int)type], transform);

        float spawnX;

        if (isLeftMove)
        {
            spawnX = rightSpawnX;
        }
        else
        {
            spawnX = leftSpawnX;
        }

        fish.isLeftMove = isLeftMove;
        fish.FlipSprite(isLeftMove);

        if (type == FishType.Spike)
        {
            Vector3 spikePos = new Vector3(spawnX, player.position.y, 1);
            fish.SetPosition(spikePos);
        }
        else
        {
            float y = Random.Range(minY, maxY);
            Vector3 pos = new Vector3(spawnX, player.position.y + y, 1);

            fish.SetPosition(pos);
        }
    }
}
