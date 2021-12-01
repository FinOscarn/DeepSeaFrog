using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCamConfiner : MonoBehaviour
{
    //플레이어의 위치값
    private Transform playerTrm;

    void Start()
    {
        //플레이어의 위치값을 GameManager에서 가져온다
        playerTrm = GameManager.instance.player.transform;
    }

    void Update()
    {
        //y값만 플레이어의 y값으로 갱신해준다
        transform.position = new Vector2(0, playerTrm.position.y);
    }
}
