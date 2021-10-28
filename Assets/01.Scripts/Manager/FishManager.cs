using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    public static FishManager instance;

    public GameObject fishPrefab;
    public GameObject bluefishPrefab;

    public const float maxY = 3f;
    public const float minY = -3f;

    public const float maxTime = 5f;
    public const float minTime = 1f;

    public readonly float spawnX = -3.4f;

    private Transform player;

    private Coroutine co;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        PoolManager.CreatePool<Fish>(fishPrefab, transform, 10);
        PoolManager.CreatePool<BlueFish>(bluefishPrefab, transform);
    }

    private void Start()
    {
        player = GameManager.instance.player.transform;

        GameManager.instance.startGame += () =>
        {
            co = StartCoroutine(CreateFish());
        };

        GameManager.instance.pause += pause =>
        {
            if (pause)
            {
                StopCoroutine(co);
            }
            else
            {
                co = StartCoroutine(CreateFish());
            }
        };
    }

    /// <summary>
    /// 파도고기를 부르는 함수입니다
    /// </summary>
    /// <param name="food">목표 먹이</param>
    public void CallFish(Food food)
    {
        BlueFish fish = PoolManager.GetItem<BlueFish>();

        Vector3 pos = new Vector3(spawnX, food.transform.position.y, 1);
        fish.Init(food);
    }

    private IEnumerator CreateFish()
    {
        while (!GameManager.instance.isGameOver)
        {
            Fish fish = PoolManager.GetItem<Fish>();

            float y = Random.Range(minY, maxY);
            Vector3 pos = new Vector3(spawnX, player.position.y + y, 1);

            fish.SetPosition(pos);

            float delay = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(delay);
        }
    }
}
