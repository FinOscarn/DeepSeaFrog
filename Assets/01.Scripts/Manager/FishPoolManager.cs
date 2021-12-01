using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPoolManager : MonoBehaviour
{
    //�̱��� ������ ���� ��������
    public static FishPoolManager instance;

    [SerializeField]
    private List<Fish> fishList = new List<Fish>(); //�������� ������ ��Ƴ��� ����Ʈ

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        //����� �����յ��� �̸� �����س��´�
        PoolManager.CreatePool<GreenFish>(fishList[0].gameObject, transform);
        PoolManager.CreatePool<OrangeFish>(fishList[1].gameObject, transform);
        PoolManager.CreatePool<PurpleFish>(fishList[2].gameObject, transform);
        PoolManager.CreatePool<GrayFish>(fishList[3].gameObject, transform);
        PoolManager.CreatePool<RedFish>(fishList[4].gameObject, transform);
        PoolManager.CreatePool<SpikeFIsh>(fishList[5].gameObject, transform);
        PoolManager.CreatePool<BoneFish>(fishList[6].gameObject, transform);
        PoolManager.CreatePool<StrongerGreenFish>(fishList[7].gameObject, transform);
    }

    public Fish GetFish(FishType type)
    {
        //������� ������ ���� �ٸ� �������� �������ش�
        switch (type)
        {
            case FishType.Green:
                return PoolManager.GetItem<GreenFish>();
            case FishType.Orange:
                return PoolManager.GetItem<OrangeFish>();
            case FishType.Purple:
                return PoolManager.GetItem<PurpleFish>();
            case FishType.Gray:
                return PoolManager.GetItem<GrayFish>();
            case FishType.Red:
                return PoolManager.GetItem<RedFish>();
            case FishType.Spike:
                return PoolManager.GetItem<SpikeFIsh>();
            case FishType.Bone:
                return PoolManager.GetItem<BoneFish>();
            case FishType.StrongerGreen:
                return PoolManager.GetItem<StrongerGreenFish>();
            default:
                Debug.Log("�������� �ʴ� FishType�Դϴ�");
                return null;
        }
    }

    public void DisableAll()
    {
        //��� ����� ���ӿ�����Ʈ�� ��Ȱ��ȭ��Ų��
        PoolManager.DisableAll<GreenFish>();
        PoolManager.DisableAll<OrangeFish>();
        PoolManager.DisableAll<PurpleFish>();
        PoolManager.DisableAll<GrayFish>();
        PoolManager.DisableAll<RedFish>();
        PoolManager.DisableAll<SpikeFIsh>();
        PoolManager.DisableAll<BoneFish>();
        PoolManager.DisableAll<StrongerGreenFish>();
    }
}
