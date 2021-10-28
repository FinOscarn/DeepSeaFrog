using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public float moveSpeed = 2;
    public float downSpeed = 3;

    [SerializeField]
    protected int maxEatCnt = 1; 
    [SerializeField]
    protected int eatCnt = 0;

    private const string PLAYER_TAG = "PLAYER";
    private const string FOOD_TAG = "FOOD";

    protected const float MAX_X = 3.6f;

    protected Player player;

    public bool isPaused = false;

    protected virtual void Start()
    {
        player = GameManager.instance.player;

        GameManager.instance.pause += pause =>
        {
            isPaused = pause;
        };
    }

    protected virtual void Update()
    {
        if (isPaused) return;

        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        transform.Translate(Vector2.down * downSpeed * Time.deltaTime);

        if(transform.position.x >= MAX_X)
        {
            gameObject.SetActive(false);
        }
    }

    protected virtual void OnEnable()
    {
        eatCnt = 0;
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
