using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FishType
{
    Green = 0,
    Orange = 1,
    Purple = 2,
    Gray = 3,
    Red = 4,
    Spike = 5,
    Bone = 6,
    StrongerGreen = 7
}

public abstract class Fish : MonoBehaviour
{
    public float moveSpeed = 2; //�¿�� �����̴� �ӵ�
    public float downSpeed = 3; //�Ʒ��� �������� �ӵ�

    public bool isLeftMove = false; //�������� �����̴���

    public FishType type; //����� ����

    [SerializeField]
    protected int maxEatCnt = 1; //�ִ�� ���� �� �ִ� ������ ����
    [SerializeField]
    protected int eatCnt = 0; //���� ���� ������ ����

    private const string PLAYER_TAG = "PLAYER"; //�÷��̾��� �±�
    private const string FOOD_TAG = "FOOD"; //������ �±�

    protected const float MAX_X = 3.6f; //�Ѿ�� ���� X��ǥ
    protected const float MIN_X = -3.6f; //�Ѿ�� ���� X��ǥ

    protected Player player; //�÷��̾�

    protected SpriteRenderer sr; //��������Ʈ ������

    public bool isPaused = false;

    private void Awake()
    {
        //��������Ʈ �������� �������ش�
        sr = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        //���Ӹ޴������ִ� �÷��̾ �������ش�
        player = GameManager.instance.player;

        //�Ͻ����� ������ �� �������� �Լ��� ������ش�
        GameManager.instance.pause += pause =>
        {
            //�Ͻ��������¸� ������� �Ͻ��������·� �ٲ��ش�
            isPaused = pause;
        };
    }

    protected virtual void Update()
    {
        //���� �Ͻ��������¶�� ����
        if (isPaused) return;

        if (isLeftMove)
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            transform.Translate(Vector2.down * downSpeed * Time.deltaTime);

            if (transform.position.x <= MIN_X)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            transform.Translate(Vector2.down * downSpeed * Time.deltaTime);

            if (transform.position.x >= MAX_X)
            {
                gameObject.SetActive(false);
            }
        }
    }

    protected virtual void OnEnable()
    {
        eatCnt = 0;
    }

    public void FlipSprite(bool isLeftMove)
    {
        sr.flipX = isLeftMove;
    }

    /// <summary>
    /// ������� ��ġ���� �ʱ�ȭ�մϴ�
    /// </summary>
    /// <param name="position">�ʱ� ��ġ��</param>
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if(eatCnt >= maxEatCnt)
        {
            
            return;
        }
        else
        {
            if (col.CompareTag(PLAYER_TAG))
            {
                OnPlayerTirgger();
            }
            else if (col.CompareTag(FOOD_TAG))
            {
                Food food = col.gameObject.GetComponent<Food>();
                OnFoodTrigger(food);
            }
        }
    }

    /// <summary>
    /// ����⿡ ���̰� �浹�ϸ� ȣ��Ǵ� �Լ��Դϴ�
    /// </summary>
    /// <param name="food">�浹�� ����</param>
    protected virtual void OnFoodTrigger(Food food)
    {
        eatCnt++;
    }

    /// <summary>
    /// ����⿡ �÷��̾ �浹�ϸ� ȣ��Ǵ� �Լ��Դϴ�
    /// </summary>
    protected virtual void OnPlayerTirgger()
    {

    }
}
