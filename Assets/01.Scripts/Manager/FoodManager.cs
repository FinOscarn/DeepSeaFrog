using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public GameObject foodPrefab;

    public const float maxX = 2.313f;
    public const float minX = -2.313f;

    public const float plusY = 10f;
    public const float createDelay = 1f;

    private bool isGameOver = false;

    private WaitForSeconds ws;
    private Transform player;

    private Coroutine co;

    private void Awake()
    {
        PoolManager.CreatePool<Food>(foodPrefab, transform, 10);
        ws = new WaitForSeconds(createDelay);
    }

    private void Start()
    {
        player = GameManager.instance.player.transform;
        GameManager.instance.playerD2ve += () =>
        {
            co = StartCoroutine(CreateFood());
        };

        GameManager.instance.pause += pause =>
        {
            if (pause)
            {
                StopCoroutine(co);
            }
            else
            {
                co = StartCoroutine(CreateFood());
            }
        };

        GameManager.instance.gameover += () =>
        {
            isGameOver = true;
        };

        GameManager.instance.reset += () =>
        {
            isGameOver = false;
            StopCoroutine(co);
            PoolManager.DisableAll<Food>();
        };
    }

    public Food GetFood(Vector3 pos)
    {
        Food food = PoolManager.GetItem<Food>();

        food.Init(pos);

        return food;
    }

    private IEnumerator CreateFood()
    {
        while (!isGameOver)
        {
            Food food = PoolManager.GetItem<Food>();
            Vector3 pos = new Vector3(Random.Range(minX, maxX), player.position.y + plusY, 0);

            food.Init(pos);

            yield return ws;
        }
    }
}
