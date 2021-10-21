using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public GameObject foodPrefab;

    public readonly float maxX = 2.313f;
    public readonly float minX = -2.313f;

    public float plusY = 1f;
    public float createDelay = 0.5f;

    private WaitForSeconds ws;
    private Transform player;

    private void Awake()
    {
        PoolManager.CreatePool<Food>(foodPrefab, transform, 10);
        ws = new WaitForSeconds(createDelay);
    }

    private void Start()
    {
        player = GameManager.instance.player.transform;

        StartCoroutine(CreateBGObject());
    }

    private IEnumerator CreateBGObject()
    {
        while (!GameManager.instance.isGameOver)
        {
            Food food = PoolManager.GetItem<Food>();
            Vector3 pos = new Vector3(Random.Range(minX, maxX), player.position.y + plusY, 0);

            food.Init(pos);

            yield return ws;
        }
    }
}
