using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����� ���� enum
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

        //���� �������� ���Ѵٸ�
        if (isLeftMove)
        {
            //��ġ�� ��� �������� �̵���Ų��
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            //��ġ�� ��� �Ʒ��� �̵���Ų��
            transform.Translate(Vector2.down * downSpeed * Time.deltaTime);

            //���� �ּ� X������ �۴ٸ�
            if (transform.position.x <= MIN_X)
            {
                //��Ȱ��ȭ��Ų��
                gameObject.SetActive(false);
            }
        }
        else
        {
            //��ġ�� ��� ���������� �̵���Ų��
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            //��ġ�� ��� �Ʒ��� �̵���Ų��
            transform.Translate(Vector2.down * downSpeed * Time.deltaTime);

            //���� �ִ� X������ ũ�ٸ�
            if (transform.position.x >= MAX_X)
            {
                //��Ȱ��ȭ��Ų��
                gameObject.SetActive(false);
            }
        }
    }

    protected virtual void OnEnable()
    {
        //���� ������ �ʱ�ȭ���ش�
        eatCnt = 0;
    }

    public void FlipSprite(bool isLeftMove)
    {
        //���� �̵������� ���� ��������Ʈ�� ���������ش�
        sr.flipX = isLeftMove;
    }

    /// <summary>
    /// ������� ��ġ���� �ʱ�ȭ�մϴ�
    /// </summary>
    /// <param name="position">�ʱ� ��ġ��</param>
    public void SetPosition(Vector3 position)
    {
        //��ġ�� �������ش�
        transform.position = position;
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        //���� �ִ�� ���� �� �ִ� �纸�� ���� ������ �Ѵٸ�
        if(eatCnt >= maxEatCnt)
        {
            //�������ش�
            return;
        }
        else
        {
            //���� �÷��̾���
            if (col.CompareTag(PLAYER_TAG))
            {
                //�÷��̾ ��Ҵٰ� �˷��ش�
                OnPlayerTirgger();
            }
            //���� ���̶��
            else if (col.CompareTag(FOOD_TAG))
            {
                //���� ���̰� ���� �����´�
                Food food = col.gameObject.GetComponent<Food>();
                //���̰� ��Ҵٰ� �˷��ش�
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
        //���� ������ �ϳ� �ø���
        eatCnt++;
    }

    /// <summary>
    /// ����⿡ �÷��̾ �浹�ϸ� ȣ��Ǵ� �Լ��Դϴ�
    /// </summary>
    protected virtual void OnPlayerTirgger()
    {

    }
}
