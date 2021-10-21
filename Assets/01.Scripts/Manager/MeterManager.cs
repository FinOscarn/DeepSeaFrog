using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterManager : MonoBehaviour
{
    public GameObject meterPrefab;

    public readonly float minusY = 12.9f;
    public readonly float startY = -1.4f;
    public readonly float xPos = 2.58f;

    public float createDelay = 1f;

    private WaitForSeconds ws;
    private Meter meter;
    private Player player;

    private void Awake()
    {
        PoolManager.CreatePool<Meter>(meterPrefab, transform, 10);
        ws = new WaitForSeconds(createDelay);
    }

    private void Start()
    {
        player = GameManager.instance.player;

        meter = PoolManager.GetItem<Meter>();
        Vector3 pos = new Vector3(xPos, startY, 0);
        meter.SetPos(pos);
    }

    private void Update()
    {
        //플레이어의 위치가 이미지의 반을 넘어가면 밑에 하나를 추가 생성
    }
}
