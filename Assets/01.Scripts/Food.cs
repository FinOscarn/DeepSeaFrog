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

        //�÷��̾�� �����Ÿ��̻� ������������ ��Ȱ��ȭ��Ų��
        if (player.transform.position.y - transform.position.y >= 10)
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// ���̸� �ʱ�ȭ ���ִ� �Լ��Դϴ�
    /// </summary>
    /// <param name="pos">�ʱ� ��ġ��</param>
    public void Init(Vector3 pos)
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        transform.position = pos;
    }

    /// <summary>
    /// �� ���̰� �÷��̾ �پ��ִ� ���������� ��ȯ�ϴ� �Լ��Դϴ�
    /// </summary>
    /// <returns>�� ���̿� �÷��̾ �پ��ִ���</returns>
    public bool IsPlayerFood()
    {
        return this == player.food;
    }

    /// <summary>
    /// ���̸� ��Ȱ��ȭ ��Ű�� �Լ��Դϴ�
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
