using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mark : MonoBehaviour
{
    //목표먹이의 위치
    public Transform targetTrm;

    private void Update()
    {
        //목표먹이가 없다면 리턴
        if (targetTrm == null) return;

        //목표먹이가 비활성화되어있다면 비활성화해준다
        if (!targetTrm.gameObject.activeSelf) Disable();

        //위치값을 목표먹이의 위치값으로 고정
        transform.position = targetTrm.position;
    }

    /// <summary>
    /// 표식을 비활성화 시키는 함수입니다
    /// </summary>
    public void Disable()
    {
        //비활성화
        gameObject.SetActive(false);
    }
}
