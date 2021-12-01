using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGObject : MonoBehaviour
{
    //배경스프라이트들
    public Sprite[] sprites;
    //스프라이트렌더러
    private SpriteRenderer spriteRenderer;
    //플레이어를 담아놓는 변수
    private Player player;

    private void Awake()
    {
        //스프라이트렌더러를 가져온다
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        //플레이어를 게임매니저에서 미리 가져온다
        player = GameManager.instance.player;
    }

    private void Update()
    {
        //플레이어와 일정거리이상 떨어져있으면 비활성화시킨다
        if (Mathf.Abs(player.transform.position.y - transform.position.y) > 15)
        {
            //비활성화시켜준다
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 배경오브젝트를 초기화해주는 함수입니다
    /// </summary>
    /// <param name="pos">초기화할 위치</param>
    public void Init(Vector3 pos)
    {
        //스프라이트를 랜덤하게 설정해주고
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        //위치값을 설정값으로 바꿔준다
        transform.position = pos;
    }
}
