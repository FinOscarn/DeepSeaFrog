using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPoolManager : MonoBehaviour
{
    //싱글톤 패턴을 위한 정적변수
    public static FishPoolManager instance;

    [SerializeField]
    private List<Fish> fishList = new List<Fish>(); //물고기들의 종류를 담아놓는 리스트

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        //물고기 프리팹들을 미리 생성해놓는다
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
        //물고기의 종류에 따라 다른 프리팹을 리턴해준다
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
                Debug.Log("존재하지 않는 FishType입니다");
                return null;
        }
    }

    public void DisableAll()
    {
        //모든 물고기 게임오브젝트를 비활성화시킨다
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
