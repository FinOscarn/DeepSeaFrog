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
    private Transform playerTrm;
    private Player player;

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
            StartCoroutine(CreateBGObject());
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
