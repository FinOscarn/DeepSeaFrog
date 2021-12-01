using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    //먹이의 스프라이트들
    public Sprite[] sprites;
    //먹이의 스프라이트 렌더러
    private SpriteRenderer spriteRenderer;

    //플레이어정보
    [SerializeField]
    protected Player player;
    //플레이어 태그를 상수로 선언
    protected const string PLAYER_TAG = "PLAYER";

    //먹이가 플레이어와 붙어있는지 여부
    public bool isCling = false;

    //원래 내려가는 속도
    public float originSpeed = 1.5f;
    //붙었을 때 내려가는 속도
    public float clingSpeed;
    //현재 내려가는 속도
    public float moveSpeed;

    //일시정지 되었는지 여부
    public bool isPaused = false;

    private void Awake()
    {
        //스프라이트 렌더러를 가져온다
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        //플레이어를 미리 게임메니저에서 가져온다
        player = GameManager.instance.player;

        //일시정지 되었을 때 해줄 일을 적어준다
        GameManager.instance.pause += pause =>
        {
            //일시정지 상태를 반영해준다
            isPaused = pause;
        };

        //현재이동속도를 원래 이동속도로 맞춰준다
        moveSpeed = originSpeed;
        //붙어있을떄 이속은 원래이속의 75%로 한다
        clingSpeed = originSpeed * 0.75f;
    }

    protected virtual void Update()
    {
        //일시정지상태라면 리턴
        if (isPaused) return;

        //만약 플레이어와 붙어있다면
        if(isCling)
        {
            //현재 이동속도는 붙었을 떄 이동속도
            moveSpeed = clingSpeed;
        }
        //아니라면
        else
        {
            //현재 이동속도는 원래 이동속도
            moveSpeed = originSpeed;
        }

        //위치값을 아래쪽으로 계속 이동시켜준다
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);

        //플레이어와 일정거리이상 떨어져있으면 비활성화시킨다
        if (player.transform.position.y - transform.position.y >= 10)
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 먹이를 초기화 해주는 함수입니다
    /// </summary>
    /// <param name="pos">초기 위치값</param>
    public void Init(Vector3 pos)
    {
        isPaused = false;
        isCling = false;
        moveSpeed = originSpeed;

        //랜덤한 먹이의 스프라이트를 가져온다
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        //위치 초기화
        transform.position = pos;
    }

    /// <summary>
    /// 이 먹이가 플레이어가 붙어있는 먹이인지를 반환하는 함수입니다
    /// </summary>
    /// <returns>이 먹이에 플레이어가 붙어있는지</returns>
    public bool IsPlayerFood()
    {
        return this == player.food;
    }

    /// <summary>
    /// 먹이를 비활성화 시키는 함수입니다
    /// </summary>
    public void Disable()
    {
        //만약 플레이어와 붙어있는 먹이가 이거라면
        if(player.food == this)
        {
            //플레이어의 붙어있는상태를 꺼준다
            player.isCling = false;
            //먹이의 붙어있는 상태를 꺼준다
            isCling = false;

            //플레이어와 붙어있는 먹이를 없에준다
            player.food = null;
        }

        //게임오브젝트를 비활성화
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //만약 플레이어와 충돌했다면
        if(collision.CompareTag(PLAYER_TAG))
        {
            //플레이어와 붙을 수 있는지 검사한다
            if (player.CanUnite(this))
            {
                //가능하다면 플레이어와 붙는다
                player.UniteFood(this);
            }
        }
    }
}
