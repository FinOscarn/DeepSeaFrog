using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    public static FishManager instance;

    public GameObject fishPrefab;

    public readonly float maxY = 3f;
    public readonly float minY = -3f;

    public readonly float maxTime = 5f;
    public readonly float minTime = 1f;

    public float spawnX = -3.4f;

    private Transform player;

    private Coroutine co;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        PoolManager.CreatePool<Fish>(fishPrefab, transform, 10);
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

    public void CallFish(Food food)
    {
        Fish fish = PoolManager.GetItem<Fish>();

        Vector3 pos = new Vector3(spawnX, food.transform.position.y, 1);
        fish.SetPosition(pos);

        fish.SetTarget(food);
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
