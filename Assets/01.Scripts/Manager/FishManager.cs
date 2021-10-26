using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    public GameObject fishPrefab;

    public readonly float maxY = 3f;
    public readonly float minY = -3f;

    public readonly float maxTime = 5f;
    public readonly float minTime = 1f;

    public float spawnX = -3.4f;

    private Transform player;

    private void Awake()
    {
        PoolManager.CreatePool<Fish>(fishPrefab, transform, 10);
    }

    private void Start()
    {
        player = GameManager.instance.player.transform;

        GameManager.instance.startGame += () =>
        {
            StartCoroutine(CreateFish());
        };
    }

    public void CallFish(Food food)
    {

    }

    private IEnumerator CreateFish()
    {
        while (!GameManager.instance.isGameOver)
        {
            Fish fish = PoolManager.GetItem<Fish>();

            float y = Random.Range(minY, maxY);
            fish.gameObject.transform.position = new Vector3(spawnX, player.position.y + y, 1);

            float delay = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(delay);
        }
    }
}
