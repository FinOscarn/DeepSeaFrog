using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGObjectManager : MonoBehaviour
{
    public GameObject bgObjectPrefab;

    public const float maxX = 2.313f;
    public const float minX = -2.313f;

    public const float minusY = 5f;
    public const float createDelay = 0.5f;

    private WaitForSeconds ws;
    private Transform playerTrm;
    private Player player;

    private Coroutine co;

    private void Awake()
    {
        PoolManager.CreatePool<BGObject>(bgObjectPrefab, transform, 10);
        ws = new WaitForSeconds(createDelay);
    }

    private void Start()
    {
        player = GameManager.instance.player;
        playerTrm = player.transform;

        GameManager.instance.startGame += () =>
        {
            co = StartCoroutine(CreateBGObject());
        };

        GameManager.instance.pause += pause =>
        {
            if(pause)
            {
                StopCoroutine(co);
            }
            else
            {
                co = StartCoroutine(CreateBGObject());
            }
        };
    }

    private IEnumerator CreateBGObject()
    {
        while (!GameManager.instance.isGameOver)
        {
            if (player.isCling)
            {
                BGObject bGObject = PoolManager.GetItem<BGObject>();
                Vector3 pos = new Vector3(Random.Range(minX, maxX), playerTrm.position.y - minusY, 0);

                bGObject.Init(pos);
            }

            yield return ws;
        }
    }
}
