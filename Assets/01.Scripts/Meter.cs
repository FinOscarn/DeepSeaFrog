using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meter : MonoBehaviour
{
    //플레이어를 담아두는 변수
    private Player player;

    //메시렌더러
    private MeshRenderer meshRenderer;
    //오프셋을 0으로 초기화
    private Vector2 offset = Vector2.zero;
    //내려가는 속도
    public float downSpeed = 0.1f;
    //올라가는 속도
    public float upSpeed = 0.1f;
    //현재 내려가는 상태인지
    public bool isDown = true;

    //일시정지 상태인지
    public bool isPaused = false;

    private void Awake()
    {
        //메시랜더러를 가져온다
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        //플레이어를 게임매니저에서 가져와 담아둔다
        player = GameManager.instance.player;

        //게임이 일시정지되었을 떄 실행할 함수를 추가해준다
        GameManager.instance.pause += pause =>
        {
            //일시정지값을 반영해준다
            isPaused = pause;
        };
    }

    private void Update()
    {
        //만약 플레이어가 붙어있는상태(내려가는상태)라면
        if(player.isCling)
        {
            //오프셋을 점점 올려준다
            offset.y += downSpeed * Time.deltaTime;
        }
        //아니라면
        else
        {
            //오프셋을 점점 내려준다
            offset.y -= upSpeed * Time.deltaTime;
        }

        //머테리얼의 offset값을 offset으로 바꿔준다
        meshRenderer.material.SetTextureOffset("_MainTex", offset);
    }
}
