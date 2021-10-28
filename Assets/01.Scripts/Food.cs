using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Player player;

    public bool isCling = false;

    public float originSpeed = 1.5f;
    public float clingSpeed;
    public float moveSpeed;

    public bool isPaused = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        player = GameManager.instance.player;

        GameManager.instance.pause += pause =>
        {
            isPaused = pause;
        };

        moveSpeed = originSpeed;
        clingSpeed = originSpeed * 0.75f;
    }

    private void Update()
    {
        if (isPaused) return;

        if(isCling)
        {
            moveSpeed = clingSpeed;
        }
        else
        {
            moveSpeed = originSpeed;
        }

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
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
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
        if(player.food == this)
        {
            player.isCling = false;
            isCling = false;

            player.food = null;
        }

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(player.CanUnite(this))
        {
            player.UniteFood(this);
        }
    }
}
