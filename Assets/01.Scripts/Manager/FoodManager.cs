using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    //���� �������� �����س��´�
    public GameObject foodPrefab;

    //���̰� �����Ǵ� x�� ������ ����� �����س��´�
    public const float maxX = 2.313f;
    public const float minX = -2.313f;

    //�÷��̾�� �󸶳� ������ �����Ǵ��� ����� �����س��´�
    public const float plusY = 10f;
    //�����ֱ⸦ ����� �����س��´�
    public const float createDelay = 1f;

    //���ӿ��� ����
    private bool isGameOver = false;

    //�ڷ�ƾ ���ð�
    private WaitForSeconds ws;
    //�÷��̾��� ��ġ
    private Transform player;

    //���̸� �����ϴ� �ڷ�ƾ
    private Coroutine co;

    private void Awake()
    {
        //Ǯ�Ŵ����� �������� �̸� �����д�
        PoolManager.CreatePool<Food>(foodPrefab, transform, 10);
        //�ڷ�ƾ ���ð��� �����ֱ�� �����س��´�
        ws = new WaitForSeconds(createDelay);
    }

    private void Start()
    {
        //�÷��̾��� Transform�� �����´�
        player = GameManager.instance.player.transform;

        //������ ���۵Ǿ�����(�÷��̾��� ���̺� ����� ������ ��) ������ �Լ��� �߰����ش�
        GameManager.instance.playerD2ve += () =>
        {
            //���̸� ����� �ڷ�ƾ�� �����ϴ� ���ÿ� ������ ��Ƶд�
            co = StartCoroutine(CreateFood());
        };

        //������ �Ͻ������Ǿ��� �� ������ �Լ��� ������ش�
        GameManager.instance.pause += pause =>
        {
            //���� �Ͻ������Ǿ��ٸ�
            if (pause)
            {
                if(co != null)
                {
                    //�ڷ�ƾ�� �������̶�� �����ش�
                    StopCoroutine(co);
                }
            }
            //�ƴ϶��
            else
            {
                //���̸� ����� �ڷ�ƾ�� �����ϴ� ���ÿ� ������ ��Ƶд�
                co = StartCoroutine(CreateFood());
            }
        };

        //���ӿ����Ǿ��� �� ������ �Լ��� �߰����ش�
        GameManager.instance.gameover += () =>
        {
            //���ӿ����� ���ش�
            isGameOver = true;
        };

        //������ ���µǾ��� �� ������ �Լ��� �߰����ش�
        GameManager.instance.reset += () =>
        {
            //���ӿ����� ���ش�
            isGameOver = false;
            //�������� �ڷ�ƾ�� �����ش�
            StopCoroutine(co);
            //��� ���̿�����Ʈ�� ��Ȱ��ȭ��Ų��
            PoolManager.DisableAll<Food>();
        };
    }

    /// <summary>
    /// ���̸� �ϳ� �������� �Լ��Դϴ�
    /// </summary>
    /// <param name="pos">���̸� ������ ��ġ</param>
    /// <returns></returns>
    public Food GetFood(Vector3 pos)
    {
        //Ǯ�Ŵ������� ���� �ϳ��� �����´�
        Food food = PoolManager.GetItem<Food>();
        //������ ��ġ�� �ʱ�ȭ���ش�
        food.Init(pos);

        //������ ���̸� ����
        return food;
    }

    /// <summary>
    /// ���̸� ��� �������ִ� ��ƾ�Դϴ�
    /// </summary>
    /// <returns></returns>
    private IEnumerator CreateFood()
    {
        //���ӿ������°� �ƴ϶��
        while (!isGameOver)
        {
            //Ǯ�Ŵ������� ���� �ϳ��� �����´�
            Food food = PoolManager.GetItem<Food>();
            //�÷��̾��� ��ġ�� ������� ���̸� ������ ��ġ�� �������� �����Ѵ�
            Vector3 pos = new Vector3(Random.Range(minX, maxX), player.position.y + plusY, 0);
            //������ ��ġ�� �ʱ�ȭ
            food.Init(pos);

            //���� �����̸�ŭ ��ٷ��ش�
            yield return ws;
        }
    }
}
