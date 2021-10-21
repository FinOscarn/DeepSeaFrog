using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGObjectManager : MonoBehaviour
{
    public GameObject bgObjectPrefab;

    public readonly float maxX = 2.313f;
    public readonly float minX = -2.313f;

    public float minusY = 1f;
    public float createDelay = 0.2f;

    private WaitForSeconds ws;
    private Transform player;

    private void Awake()
    {
        PoolManager.CreatePool<BGObject>(bgObjectPrefab, transform, 10);
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
            BGObject bGObject = PoolManager.GetItem<BGObject>();
            Vector3 pos = new Vector3(Random.Range(minX, maxX), player.position.y - minusY, 0);

            bGObject.Init(pos);

            yield return ws;
        }
    }
}
