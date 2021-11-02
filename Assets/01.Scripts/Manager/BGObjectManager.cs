using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGObjectManager : MonoBehaviour
{
    public GameObject bgObjectPrefab; //��������Ʈ ������

    public const float maxX = 2.313f; //�ִ� X��ǥ
    public const float minX = -2.313f; //�ּ� Y ��ǥ

    public const float minusY = 5f; //�÷��̾�κ��� �󸶳� �ؿ� �����Ǵ���
    public const float createDelay = 0.5f; //���� �ֱ�

    private WaitForSeconds ws; //�޸� ������ ���� �ν��Ͻ�
    private Transform playerTrm; //�÷��̾��� ��ġ��
    private Player player; //�÷��̾�

    private Coroutine co;

    private void Awake()
    {
        //Ǯ�Ŵ����� ��������Ʈ�� �̸� �����س��´�
        PoolManager.CreatePool<BGObject>(bgObjectPrefab, transform, 10);
        //�����ֱ⿡ ���� Ws�� ����
        ws = new WaitForSeconds(createDelay);
    }

    private void Start()
    {
        //���ӸŴ������� �÷��̾ �����´�
        player = GameManager.instance.player;
        //�÷��̾��� Ʈ�������� ���� �����س��´�
        playerTrm = player.transform;

        //������ ���۵Ǿ��� �� ������ �Լ��� ������ش�
        GameManager.instance.startGame += () =>
        {
            //��������Ʈ�� ����� �ڷ�ƾ�� �������ְ� ������ ��Ƶд�
            co = StartCoroutine(CreateBGObject());
        };

        GameManager.instance.pause += pause =>
        {
            //���� �Ͻ��������
            if(pause)
            {
                //�������̴� �ڷ�ƾ�� �����ش�
                StopCoroutine(co);
            }
            else
            {
                //��������Ʈ�� ����� �ڷ�ƾ�� �������ְ� ������ ��Ƶд�
                co = StartCoroutine(CreateBGObject());
            }
        };
    }

    /// <summary>
    /// ��������Ʈ�� ����ؼ� ������ִ� �Լ��Դϴ�
    /// </summary>
    /// <returns></returns>
    private IEnumerator CreateBGObject()
    {
        //���� ���ӿ������°� �ƴ϶��
        while (!GameManager.instance.isGameOver)
        {
            //���� �÷��̾ ���̿� �پ��ִٸ�
            if (player.isCling)
            {
                //Ǯ�޴������� ��������Ʈ �ϳ��� �����´�
                BGObject bGObject = PoolManager.GetItem<BGObject>();
                //X��ġ���� �������� �̾��ְ� �÷��̾��� ��ġ���� minusY�� ���� ��ġ��
                Vector3 pos = new Vector3(Random.Range(minX, maxX), playerTrm.position.y - minusY, 0);

                //��������Ʈ�� �ʱ�ȭ ���ش�
                bGObject.Init(pos);
            }

            //createDelay �Ŀ� �ڷ�ƾ�� �ٽ� ����ǰ� ���ش�
            yield return ws;
        }
    }
}
