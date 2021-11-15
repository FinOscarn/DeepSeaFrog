using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    public static FishManager instance; //�̱��� ������ ���� �ν��Ͻ�

    public BlueFish bluefishPrefab; //�ĵ������ ������

    public Soil soilPrefab; //������� �к� ������
    public Mark markPrefab; //�ĵ���Ⱑ ���ö� ��Ÿ���� ��ũ ������

    public const float maxY = 3f; //����Ⱑ �����Ǵ� �ִ� Y��ǥ
    public const float minY = -3f; //����Ⱑ �����Ǵ� �ּ� Y��ǥ

    public const float maxTime = 3f; //����Ⱑ �����Ǵ� �ִ� �ð�
    public const float minTime = 1f; //����Ⱑ �����Ǵ� �ּ� �ð�

    public readonly float leftSpawnX = -3.4f; //����Ⱑ ���ʿ��� ������ ���� ��ǥ
    public readonly float rightSpawnX = 3.4f; //����Ⱑ �����ʿ��� ������ ���� ��ǥ

    private bool isGameOver = false;

    private Transform player; //�÷��̾��� Ʈ������

    private Coroutine co; //����⸦ �����ϴ� �ڷ�ƾ

    private void Awake()
    {
        //���� �ν��Ͻ��� ���ٸ�
        if(instance == null)
        {
            //�ڱ��ڽ��� �ν��Ͻ��� �ִ´�
            instance = this;
        }

        //Ǯ�޴����� �������� �̸� �����д�
        PoolManager.CreatePool<BlueFish>(bluefishPrefab.gameObject, transform, 2);

        PoolManager.CreatePool<Soil>(soilPrefab.gameObject, transform, 10);
        PoolManager.CreatePool<Mark>(markPrefab.gameObject, transform, 2);
    }

    private void Start()
    {
        //�÷��̾��� Ʈ�������� ���Ӹ޴����� �ִ� �÷��̾��� Ʈ���������� �����´�
        player = GameManager.instance.player.transform;

        //������ ���۵Ǿ�����(�÷��̾��� ���̺� ����� ������ ��) ������ �Լ��� �߰����ش�
        GameManager.instance.playerD2ve += () =>
        {
            //����⸦ �����ϴ� �ڷ�ƾ�� �������ܰ� ���ÿ� ������ ��Ƶд�
            co = StartCoroutine(CreateFishRoutine());
        };

        //������ �Ͻ������Ǿ��� �� ������ �Լ��� �߰����ش�
        GameManager.instance.pause += pause =>
        {
            //���� �Ͻ������Ǿ��ٸ�
            if (pause)
            {
                //����Ⱑ �����Ǵ� �ڷ�ƾ�� �����ش�
                StopCoroutine(co);
            }
            else
            {   //����⸦ �����ϴ� �ڷ�ƾ�� �������ܰ� ���ÿ� ������ ��Ƶд�
                co = StartCoroutine(CreateFishRoutine());
            }
        };

        GameManager.instance.gameover += () =>
        {
            isGameOver = true;
        };

        GameManager.instance.reset += () =>
        {
            isGameOver = false;
            FishPoolManager.instance.DisableAll();

            PoolManager.DisableAll<BlueFish>();

            PoolManager.DisableAll<Soil>();
            PoolManager.DisableAll<Mark>();
        };
    }

    /// <summary>
    /// �ĵ���⸦ �θ��� �Լ��Դϴ�
    /// </summary>
    /// <param name="food">��ǥ ����</param>
    public void CallBlueFish(Food food)
    {
        //Ǯ�Ŵ������� �ĵ���� �ϳ��� �����´�
        BlueFish fish = PoolManager.GetItem<BlueFish>();

        //bool ���� ������ �̴´�
        bool isLeftMove = (Random.value > 0.5f);

        //�ĵ���⸦ �ʱ�ȸ���ش�
        fish.Init(food, isLeftMove);
        //�ĵ������ �̵������� �����ش�
        fish.isLeftMove = isLeftMove;
        //�ĵ������ ��������Ʈ�� ���������� ���θ� �����ش�
        fish.FlipSprite(isLeftMove);
    }

    /// <summary>
    /// BornFish���� ����ϴ� �к��� �����ϴ� �Լ�
    /// </summary>
    /// <param name="position">BornFish�� ��ġ��</param>
    /// <returns></returns>
    public Soil GetSoil(Vector3 position)
    {
        //Ǯ�Ŵ������� �к��� �����´�
        Soil soil = PoolManager.GetItem<Soil>();
        //�к��� ��ġ�� ���� ��ġ�� �������ش�
        soil.transform.position = position;

        //�к��� ����
        return soil;
    }

    /// <summary>
    /// �ĵ���⿡�� ����ϴ� Mark�� �����ϴ� �Լ�
    /// </summary>
    /// <param name="food">��ǥ ����</param>
    /// <returns></returns>
    public Mark GetMark(Food food)
    {
        //Ǯ�Ŵ������� ��ũ�� �ϳ� �����´�
        Mark mark = PoolManager.GetItem<Mark>();
        //��ũ�� ��ǥ Ʈ�������� ���� Food�� Ʈ���������� ���ش�
        mark.targetTrm = food.gameObject.transform;

        //��ũ�� ����
        return mark;
    }

    /// <summary>
    /// ����ؼ� ����⸦ �������ִ� ��ƾ�Դϴ�
    /// </summary>
    /// <returns></returns>
    private IEnumerator CreateFishRoutine()
    {
        //���� ���ӿ����� �ƴ϶��
        while (!isGameOver)
        {
            //�������� bool���� �̴´�
            bool randBool = (Random.value > 0.5f);

            //����Ⱑ �¿��� �����Ǵ��� �쿡�� �����Ǵ����� �Ű������� ��� ������
            CreateRandomFish(randBool);

            //����Ⱑ �����Ǵ� �����̸� �������� �̾��ش�
            float delay = Random.Range(minTime, maxTime);
            //�������� ���� �ð���ŭ ����ߴٰ� �ٽ� �����Ѵ�
            yield return new WaitForSeconds(delay);
        }
    }

    /// <summary>
    /// �������� ����⸦ �����ϴ� �Լ��Դϴ�
    /// </summary>
    private void CreateRandomFish(bool isLeftMove)
    {
        //FishType�� ù��°���� ���������� �ϳ��� �������� �̴´�
        FishType type = (FishType)Random.Range((int)FishType.Green, (int)FishType.StrongerGreen);

        //���߿� Ǯ������ �ٲ���
        //FishType�� �´� ����⸦ �ϳ� �����Ѵ�
        Fish fish = FishPoolManager.instance.GetFish(type);

        //����Ⱑ ������ ��ǥ
        float spawnX;

        //�������� �����δٸ�
        if (isLeftMove)
        {   
            //������ǥ�� ���������� ���ش�
            spawnX = rightSpawnX;
        }
        else
        {
            //������ǥ�� �������� ���ش�
            spawnX = leftSpawnX;
        }

        //�������� �����̴��� ���θ� �ٲ��ش�
        fish.isLeftMove = isLeftMove;
        //��������Ʈ�� ���������� ���θ� �����ش�
        fish.FlipSprite(isLeftMove);

        //���� ������
        if (type == FishType.Spike)
        {
            //������ ��ġ�� �÷��̾��� ��ġ�� ���������� �����ȴ�
            Vector3 spikePos = new Vector3(spawnX, player.position.y, 1);
            //������ ��ġ�� �������ش�
            fish.SetPosition(spikePos);
        }
        else
        {
            //�ִ밪�� �ּҰ� ���̿��� y��ǥ�� �̴´�
            float y = Random.Range(minY, maxY);
            //�÷��̾��� �����ǿ� �Ʊ� ���� y��ǥ�� ���� ��ġ��
            Vector3 pos = new Vector3(spawnX, player.position.y + y, 1);
            //������� ��ġ�� �������ش�
            fish.SetPosition(pos);
        }
    }
}
